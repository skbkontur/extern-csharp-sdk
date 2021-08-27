using System;
using System.Buffers;
using System.Diagnostics;
using Kontur.Extern.Client.Http.Exceptions;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.Buffers
{
    internal sealed class PooledByteBufferWriter : IBufferWriter<byte>, IDisposable
    {
        private readonly ArrayPool<byte> bytesPool;
        private byte[]? rentedBuffer;
        private int writtenLength;

        private const int MinimumBufferSize = 256;

        public PooledByteBufferWriter(ArrayPool<byte> bytesPool, int initialCapacity)
        {
            if (initialCapacity <= 0)
                throw Errors.ValueShouldBeGreaterThanZero(nameof(initialCapacity), initialCapacity);
            
            this.bytesPool = bytesPool;
            rentedBuffer = bytesPool.Rent(initialCapacity);
            writtenLength = 0;
        }

        public ReadOnlyMemory<byte> WrittenMemory => rentedBuffer.AsMemory(0, writtenLength);

        public void Dispose()
        {
            if (rentedBuffer is null)
                return;

            try
            {
                rentedBuffer.AsSpan(0, writtenLength).Clear();
            }
            finally
            {
                bytesPool.Return(rentedBuffer);
                writtenLength = 0;
                rentedBuffer = null!;
            }
        }

        public void Advance(int count)
        {
            if (count < 0)
                throw Errors.ValueShouldBeGreaterOrEqualTo(nameof(count), count, 0);
            
            writtenLength += count;
        }

        public Memory<byte> GetMemory(int sizeHint = 0)
        {
            if (sizeHint < 0)
                throw Errors.ValueShouldBeGreaterOrEqualTo(nameof(sizeHint), sizeHint, 0);
            
            CheckAndResizeBuffer(sizeHint);
            return rentedBuffer.AsMemory(writtenLength);
        }

        public Span<byte> GetSpan(int sizeHint = 0)
        {
            if (sizeHint < 0)
                throw Errors.ValueShouldBeGreaterOrEqualTo(nameof(sizeHint), sizeHint, 0);
            
            CheckAndResizeBuffer(sizeHint);
            return rentedBuffer.AsSpan(writtenLength);
        }

        private void CheckAndResizeBuffer(int sizeHint)
        {
            if (sizeHint == 0)
                sizeHint = MinimumBufferSize;

            var availableSpace = rentedBuffer!.Length - writtenLength;
            if (sizeHint > availableSpace)
            {
                var currentLength = rentedBuffer.Length;
                var growBy = Math.Max(sizeHint, currentLength);

                var newSize = currentLength + growBy;

                if ((uint) newSize > int.MaxValue)
                {
                    newSize = currentLength + sizeHint;
                    if ((uint) newSize > int.MaxValue)
                    {
                        throw new OutOfMemoryException($"Cannot allocate a buffer of size {(uint) newSize}.");
                    }
                }

                byte[] oldBuffer = rentedBuffer;
                rentedBuffer = bytesPool.Rent(newSize);

                try
                {
                    Debug.Assert(oldBuffer.Length >= writtenLength);
                    Debug.Assert(rentedBuffer.Length >= writtenLength);

                    var previousBuffer = oldBuffer.AsSpan(0, writtenLength);
                    previousBuffer.CopyTo(rentedBuffer);
                    previousBuffer.Clear();
                }
                finally
                {
                    bytesPool.Return(oldBuffer);
                }
            }

            Debug.Assert(rentedBuffer.Length - writtenLength > 0);
            Debug.Assert(rentedBuffer.Length - writtenLength >= sizeHint);
        } 
    }
}