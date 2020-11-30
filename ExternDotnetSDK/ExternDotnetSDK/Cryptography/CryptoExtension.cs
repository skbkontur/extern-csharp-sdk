using System;
using System.Collections.Generic;
using System.Text;

namespace Kontur.Extern.Client.Cryptography
{
    public static class BytesExtension
    {
        /// <summary>
        /// Сравнение двух массивов байт
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static int Compare(this byte[] d1, byte[] d2)
        {
            if (d1 == d2) return 0;
            if (d1 == null) return -1;
            if (d2 == null) return 1;
            int data1Length = d1.Length;
            int data2Length = d2.Length;
            for (int i = 0; i < Math.Min(data1Length, data2Length); ++i)
            {
                int comparison = d1[i].CompareTo(d2[i]);
                if (comparison != 0) return comparison;
            }
            return data1Length.CompareTo(data2Length);
        }
    }
}
