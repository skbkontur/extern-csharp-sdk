#nullable enable
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Kontur.Extern.Client.Common;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.ApiLevel.Clients.Contents
{
    internal class ChunkContentStream : Stream
    {
        public static async Task<Stream> CreateAsync(Func<(long from, long to), Task<(ArraySegment<byte> contentPart, long totalLength)>> downloadContentPartAsync, int partSize)
        {
            if (downloadContentPartAsync == null)
                throw new ArgumentNullException(nameof(downloadContentPartAsync));
            if (partSize <= 0)
                throw Errors.ValueShouldBeGreaterThanZero(nameof(partSize), partSize);

            var (firstContentPart, totalLength) = await downloadContentPartAsync((0, partSize - 1)).ConfigureAwait(false);
            if (totalLength == firstContentPart.Count)
            {
                return firstContentPart.ToMemoryStreamWithPublicBuffer();
            }

            return new ChunkContentStream(downloadContentPartAsync, totalLength, firstContentPart);
        }
        
        private readonly long length;
        private readonly Func<(long from, long to), Task<ContentPart>> downloadContentPartAsync;
        
        private ContentPart currentPart;
        private long position;
        private Task<ContentPart>? nextContentPart;

        private ChunkContentStream(
            Func<(long from, long to), Task<(ArraySegment<byte> contentPart, long totalLength)>> downloadContentPartAsync,
            long length,
            ArraySegment<byte> firstPartContent)
        {
            if (length <= 0)
                throw Errors.ValueShouldBeGreaterThanZero(nameof(length), length);
            if (length < firstPartContent.Count)
                throw Errors.ValueShouldBeGreaterOrEqualTo(nameof(length), length, firstPartContent.Count);

            this.length = length;

            this.downloadContentPartAsync = DownloadContentPart;
            currentPart = new ContentPart(firstPartContent, 0, firstPartContent.Count - 1, length);

            var nextRange = currentPart.NextRange;
            if (nextRange.HasValue)
            {
                nextContentPart = this.downloadContentPartAsync(nextRange.Value);
            }

            async Task<ContentPart> DownloadContentPart((long from, long to) range)
            {
                var (contentPart, totalLength) = await downloadContentPartAsync(range).ConfigureAwait(false);
                return new ContentPart(contentPart, range.from, range.to, totalLength);
            }
        }

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => false;
        public override long Length => length;
        public override long Position
        {
            get => position;
            set
            {
                if (value < 0)
                    throw Errors.ValueShouldBePositive(nameof(Position), value);
                if (value > length)
                    throw Errors.ValueCannotBeGreaterOrEqualThenAnother(nameof(Position), nameof(Length), value, length);

                position = value;
            }
        }

        public override void Flush()
        {
        }

        public override long Seek(long offset, SeekOrigin origin) =>
            Position = origin switch
            {
                SeekOrigin.Begin => offset,
                SeekOrigin.Current => position + offset,
                SeekOrigin.End => length + offset,
                _ => position
            };

        public override void SetLength(long value) => throw new NotSupportedException();

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback? callback, object state)
        {
            var readTask = ReadAsync(buffer, offset, count);

            var tcs = new TaskCompletionSource<int>(state, TaskCreationOptions.RunContinuationsAsynchronously);
            readTask.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        tcs.TrySetException(t.Exception!.InnerExceptions);
                    }
                    else if (t.IsCanceled)
                    {
                        tcs.TrySetCanceled();
                    }
                    else
                    {
                        tcs.TrySetResult(t.GetAwaiter().GetResult());
                    }

                    callback?.Invoke(tcs.Task);
                },
                TaskScheduler.Default
            );
            return tcs.Task;
        }

        public override int EndRead(IAsyncResult asyncResult) => ((Task<int>) asyncResult).GetAwaiter().GetResult();

        public override int Read(byte[] buffer, int offset, int count) => ReadAsync(buffer, offset, count).GetAwaiter().GetResult();

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw Errors.ValueShouldBePositive(nameof(offset), offset);
            if (offset > buffer.Length)
                throw Errors.TheOffsetCannotExceedBufferLength(nameof(offset), offset, buffer.Length);
            if (count < 0)
                throw Errors.ValueShouldBePositive(nameof(count), count);
            if (count + offset > buffer.Length)
                throw Errors.TheCountCannotBeOutOfBuffer(nameof(count), count, buffer.Length);

            var read = 0;
            while (count > 0)
            {
                if (!currentPart.ContainsPosition(position) && position < length)
                {
                    await ReadNextPart().ConfigureAwait(false);
                }

                var dstOffset = offset + read;
                var copiedBytes = CopyCachedBytesToBuffer(currentPart, buffer, count, dstOffset, position);
                if (copiedBytes == null)
                    break;
                
                read += copiedBytes.Value;
                count -= copiedBytes.Value;
                position += copiedBytes.Value;
            }

            return read;

            async Task ReadNextPart()
            {
                var partSize = currentPart.Length;
                var to = position + (partSize - position%partSize) - 1;

                if (currentPart.PositionBelongsToTheNextPart(position))
                {
                    if (nextContentPart == null)
                        throw Errors.TheContentIsAlreadyEndedAndNextPartCannotBeLoaded();

                    currentPart = await nextContentPart.ConfigureAwait(false);
                }
                else
                {
                    currentPart = await downloadContentPartAsync((position, to)).ConfigureAwait(false);
                }

                var nextRange = currentPart.NextRange;
                if (nextRange.HasValue)
                {
                    nextContentPart = downloadContentPartAsync(nextRange.Value);
                }
            }

            static int? CopyCachedBytesToBuffer(in ContentPart contentPart, byte[] buffer, int countInBuffer, int bufferOffset, long position)
            {
                var remainBytes = contentPart.GetRemainingBytesAt(position);
                if (remainBytes.Array == null)
                    return null;
                
                var needToCopyBytesCount = Math.Min(remainBytes.Count, countInBuffer);
                Buffer.BlockCopy(
                    remainBytes.Array,
                    remainBytes.Offset,
                    buffer,
                    bufferOffset,
                    needToCopyBytesCount);
                return needToCopyBytesCount;
            }
        }

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        private class ContentPart
        {
            private readonly ArraySegment<byte> bytes;
            private readonly ContentRange range;
            
            public ContentPart(ArraySegment<byte> bytes, long startOffset, long endOffset, long totalLength)
            {
                if (bytes.Array == null)
                    throw Errors.ContentPartCannotHaveEmptyBytes(nameof(bytes));
                this.bytes = bytes;
                range = new ContentRange(startOffset, endOffset, totalLength);
            }
            
            public (long from, long to)? NextRange => range.Next;
            public long Length => range.Length;

            public bool ContainsPosition(long position) => range.ContainsPosition(position);

            public ArraySegment<byte> GetRemainingBytesAt(long position)
            {
                var offset = (int)(position - range.From);
                var remainCount = bytes.Count - offset;
                return remainCount <= 0 
                    ? default 
                    : new ArraySegment<byte>(bytes.Array!, bytes.Offset + offset, remainCount);
            }

            public bool PositionBelongsToTheNextPart(long position) => 
                range.InTheNextPart(position);
        }

        private readonly struct ContentRange
        {
            public ContentRange(long from, long to, long totalLength)
            {
                if (from < 0)
                    throw Errors.ValueShouldBeGreaterThanZero(nameof(from), from);
                if (to < 0)
                    throw Errors.ValueShouldBeGreaterThanZero(nameof(from), from);
                if (from > to)
                    throw Errors.ValueCannotBeLessThenAnother(nameof(from), nameof(to), from, to);
                if (to >= totalLength)
                    throw Errors.ValueCannotBeGreaterOrEqualThenAnother(nameof(to), nameof(totalLength), to, totalLength);
                
                From = from;
                To = to;
                TotalLength = totalLength;
            }

            public long From { get; }
            public long To { get; }
            public long Length => To - From + 1;
            public long TotalLength { get; }

            public bool ContainsPosition(long position) => 
                From <= position && position <= To;

            public bool InTheNextPart(long position) => 
                position >= To + 1 && position <= To + Length;

            public ContentRange? Next
            {
                get
                {
                    if (IsLastPart)
                        return null;

                    var to = Math.Min(To + Length, TotalLength - 1);
                    return new(To + 1, to, TotalLength);
                }
            }

            public bool IsLastPart => To == TotalLength - 1;

            public static implicit operator (long from, long to)(ContentRange range) => (range.From, range.To);
        }
    }
}