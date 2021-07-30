using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Benchmarks
{
    public class ZeroBytesComparisonBenchmark
    {
        private byte[] zeroBytes = null!;
        private byte[] nonZeroBytes = null!;

        [GlobalSetup]
        public void AllocateBytes()
        {
            zeroBytes = new byte[4096];
            nonZeroBytes = new byte[4096];
            Array.Fill(nonZeroBytes, byte.MaxValue);
        }

        [Benchmark]
        public bool Simple_For_ZeroBytes() => SimpleIsZero(zeroBytes);

        [Benchmark]
        public bool AsLongSpan_For_ZeroBytes() => AsLongIsZero(zeroBytes);
        
        [Benchmark]
        public bool ByFixedLongUnrolled_ZeroBytes() => ByFixedLongUnrolledIsZero(zeroBytes);
        
        [Benchmark]
        public bool Mixed_ZeroBytes() => BytesComparer.IsZero(zeroBytes);
        
        [Benchmark]
        public bool Simple_For_NonZeroBytes() => SimpleIsZero(nonZeroBytes);

        [Benchmark]
        public bool AsLongSpan_For_NonZeroBytes() => AsLongIsZero(nonZeroBytes);
        
        [Benchmark]
        public bool ByFixedLongUnrolled_NonZeroBytes() => ByFixedLongUnrolledIsZero(nonZeroBytes);
        
        [Benchmark]
        public bool Mixed_NonZeroBytes() => BytesComparer.IsZero(nonZeroBytes);

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        private static bool SimpleIsZero(byte[] bytes)
        {
            for (var i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }
        
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        private static bool AsLongIsZero(ReadOnlySpan<byte> bytes)
        {
            var longs = MemoryMarshal.Cast<byte, long>(bytes);
            for (var i = 0; i < longs.Length; i++)
            {
                if (longs[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static unsafe bool ByFixedLongUnrolledIsZero(byte[] data)
        {
            fixed (byte* bytes = data)
            {
                var len = data.Length;
                var rem = len%(sizeof (long)*16);
                var b = (long*) bytes;
                var e = (long*) (bytes + len - rem);

                while (b < e)
                {
                    if ((*b | *(b + 1) | *(b + 2) | *(b + 3) | *(b + 4) |
                         *(b + 5) | *(b + 6) | *(b + 7) | *(b + 8) |
                         *(b + 9) | *(b + 10) | *(b + 11) | *(b + 12) |
                         *(b + 13) | *(b + 14) | *(b + 15)) != 0)
                    {
                        return false;
                    }

                    b += 16;
                }

                for (var i = 0; i < rem; i++)
                {
                    if (data[len - 1 - i] != 0)
                        return false;
                }

                return true;
            }
        }
    }
}