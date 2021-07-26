using System;
using Kontur.Extern.Client.Common.Exceptions;

namespace Kontur.Extern.Client.Common.Time
{
    /// <summary>
    /// Represents a positive time interval
    /// </summary>
    public readonly struct TimeInterval
    {
        private readonly TimeSpan timeSpan;

        public TimeInterval(TimeSpan timeSpan)
        {
            if (timeSpan.Ticks < 0)
                throw Errors.TimeSpanShouldBePositive(nameof(timeSpan), timeSpan);
            this.timeSpan = timeSpan;
        }

        public bool IsEmpty => timeSpan == TimeSpan.Zero;

        public TimeSpan Unwrap() => timeSpan;
        
        public bool Equals(TimeInterval other) => timeSpan.Equals(other.timeSpan);

        public override bool Equals(object obj) => obj is TimeInterval other && Equals(other);

        public override int GetHashCode() => timeSpan.GetHashCode();

        public override string ToString() => timeSpan.ToString();

        #region Operators between intervals

        /// <summary>Adds two specified <see cref="T:TimeInterval" /> instances.</summary>
        /// <param name="t1">The first time interval to add.</param>
        /// <param name="t2">The second time interval to add.</param>
        /// <exception cref="T:System.OverflowException">The resulting <see cref="T:TimeInterval" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
        /// <returns>An object whose value is the sum of the values of <paramref name="t1" /> and <paramref name="t2" />.</returns>
        public static TimeInterval operator+(in TimeInterval t1, in TimeInterval t2) => t1.timeSpan + t2.timeSpan;

        /// <summary>Indicates whether two <see cref="T:TimeInterval" /> instances are equal.</summary>
        /// <param name="t1">The first time interval to compare.</param>
        /// <param name="t2">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, <see langword="false" />.</returns>
        public static bool operator==(in TimeInterval t1, in TimeInterval t2) => t1.timeSpan == t2.timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is greater than another specified <see cref="T:TimeInterval" />.</summary>
        /// <param name="t1">The first time interval to compare.</param>
        /// <param name="t2">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="t1" /> is greater than the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
        public static bool operator>(TimeInterval t1, TimeInterval t2) => t1.timeSpan > t2.timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is greater than or equal to another specified <see cref="T:TimeInterval" />.</summary>
        /// <param name="t1">The first time interval to compare.</param>
        /// <param name="t2">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="t1" /> is greater than or equal to the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
        public static bool operator>=(TimeInterval t1, TimeInterval t2) => t1 >= t2;

        /// <summary>Indicates whether two <see cref="T:TimeInterval" /> instances are not equal.</summary>
        /// <param name="t1">The first time interval to compare.</param>
        /// <param name="t2">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are not equal; otherwise, <see langword="false" />.</returns>
        public static bool operator!=(TimeInterval t1, TimeInterval t2) => t1.timeSpan != t2.timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is less than another specified <see cref="T:TimeInterval" />.</summary>
        /// <param name="t1">The first time interval to compare.</param>
        /// <param name="t2">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="t1" /> is less than the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
        public static bool operator<(TimeInterval t1, TimeInterval t2) => t1.timeSpan < t2.timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is less than or equal to another specified <see cref="T:TimeInterval" />.</summary>
        /// <param name="t1">The first time interval to compare.</param>
        /// <param name="t2">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="t1" /> is less than or equal to the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
        public static bool operator<=(TimeInterval t1, TimeInterval t2) => t1.timeSpan <= t2.timeSpan;

        /// <summary>Subtracts a specified <see cref="T:TimeInterval" /> from another specified <see cref="T:TimeInterval" />.</summary>
        /// <param name="t1">The minuend.</param>
        /// <param name="t2">The subtrahend.</param>
        /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
        /// <returns>An object whose value is the result of the value of <paramref name="t1" /> minus the value of <paramref name="t2" />.</returns>
        public static TimeInterval operator-(TimeInterval t1, TimeInterval t2) => t1 - t2.timeSpan;
        
        // /// <summary>Returns a new <see cref="T:System.Double" /> value which is the result of division of <paramref name="t1" /> instance and the specified <paramref name="t2" />.</summary>
        // /// <param name="t1">Divident or the value to be divided.</param>
        // /// <param name="t2">The value to be divided by.</param>
        // /// <returns>A new value that represents result of division of <paramref name="t1" /> instance by the value of the <paramref name="t2" />.</returns>
        // public static double operator/(in TimeInterval t1, in TimeInterval t2) => t1.timeSpan/t2.timeSpan;
        
        // /// <summary>Returns a new <see cref="T:TimeInterval" /> object whose value is the result of multiplying the specified <paramref name="factor" /> and the specified <paramref name="timeInterval" /> instance.</summary>
        // /// <param name="factor">The value to be multiplied by.</param>
        // /// <param name="timeInterval">The value to be multiplied.</param>
        // /// <returns>A new object that represents the value of the specified <paramref name="factor" /> multiplied by the value of the specified <paramref name="timeInterval" /> instance.</returns>
        // public static TimeInterval operator*(double factor, TimeInterval timeInterval) => factor*timeInterval.timeSpan;
        //
        // /// <summary>Returns a new <see cref="T:TimeInterval" /> object whose value is the result of multiplying the specified <paramref name="timeInterval" /> instance and the specified <paramref name="factor" />.</summary>
        // /// <param name="timeInterval">The value to be multiplied.</param>
        // /// <param name="factor">The value to be multiplied by.</param>
        // /// <returns>A new object that represents the value of the specified <paramref name="timeInterval" /> instance multiplied by the value of the specified <paramref name="factor" />.</returns>
        // public static TimeInterval operator*(TimeInterval timeInterval, double factor) => timeInterval.timeSpan*factor;

        #endregion

        #region Interoperability with time spans

        public static implicit operator TimeInterval(TimeSpan timeSpan) => new(timeSpan);

        /// <summary>Adds two specified <see cref="T:TimeInterval" /> instances.</summary>
        /// <param name="timeInterval">The first time interval to add.</param>
        /// <param name="timeSpan">The second time interval to add.</param>
        /// <exception cref="T:System.OverflowException">The resulting <see cref="T:TimeInterval" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
        /// <returns>An object whose value is the sum of the values of <paramref name="timeInterval" /> and <paramref name="timeSpan" />.</returns>
        public static TimeInterval operator+(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan + timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is equal to a specified <see cref="T:System.TimeSpan" />.</summary>
        /// <param name="timeInterval">The first time interval to compare.</param>
        /// <param name="timeSpan">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the values of <paramref name="timeInterval" /> and <paramref name="timeSpan" /> are equal; otherwise, <see langword="false" />.</returns>
        public static bool operator==(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan == timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is greater than a specified <see cref="T:System.TimeSpan" />.</summary>
        /// <param name="timeInterval">The first time interval to compare.</param>
        /// <param name="timeSpan">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="timeInterval" /> is greater than the value of <paramref name="timeSpan" />; otherwise, <see langword="false" />.</returns>
        public static bool operator>(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan > timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is greater than or equal to a specified <see cref="T:System.TimeSpan" />.</summary>
        /// <param name="timeInterval">The first time interval to compare.</param>
        /// <param name="timeSpan">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="timeInterval" /> is greater than or equal to the value of <paramref name="timeSpan" />; otherwise, <see langword="false" />.</returns>
        public static bool operator>=(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan >= timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:TimeInterval" /> is not equal to a specified <see cref="T:System.TimeSpan" />.</summary>
        /// <param name="timeInterval">The first time interval to compare.</param>
        /// <param name="timeSpan">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the values of <paramref name="timeInterval" /> and <paramref name="timeSpan" /> are not equal; otherwise, <see langword="false" />.</returns>
        public static bool operator!=(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan != timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than a specified <see cref="T:System.TimeSpan" />.</summary>
        /// <param name="timeInterval">The first time interval to compare.</param>
        /// <param name="timeSpan">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="timeInterval" /> is less than the value of <paramref name="timeSpan" />; otherwise, <see langword="false" />.</returns>
        public static bool operator<(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan < timeSpan;

        /// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than or equal to a specified <see cref="T:System.TimeSpan" />.</summary>
        /// <param name="timeInterval">The first time interval to compare.</param>
        /// <param name="timeSpan">The second time interval to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="timeInterval" /> is less than or equal to the value of <paramref name="timeSpan" />; otherwise, <see langword="false" />.</returns>
        public static bool operator<=(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan <= timeSpan;

        /// <summary>Subtracts a specified <see cref="T:System.TimeSpan" /> from a specified <see cref="T:System.TimeSpan" />.</summary>
        /// <param name="timeInterval">The minuend.</param>
        /// <param name="timeSpan">The subtrahend.</param>
        /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
        /// <returns>An object whose value is the result of the value of <paramref name="timeInterval" /> minus the value of <paramref name="timeSpan" />.</returns>
        public static TimeInterval operator-(TimeInterval timeInterval, TimeSpan timeSpan)
        {
            var result = timeInterval.timeSpan - timeSpan;
            return result < TimeSpan.Zero ? default(TimeInterval) : result;
        }

        // /// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the result of multiplying the specified <paramref name="factor" /> and the specified <paramref name="timeInterval" /> instance.</summary>
        // /// <param name="factor">The value to be multiplied by.</param>
        // /// <param name="timeInterval">The value to be multiplied.</param>
        // /// <returns>A new object that represents the value of the specified <paramref name="factor" /> multiplied by the value of the specified <paramref name="timeInterval" /> instance.</returns>
        // public static TimeInterval operator*(double factor, TimeInterval timeInterval) => factor*timeInterval.timeSpan;
        //
        // /// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the result of multiplying the specified <paramref name="timeInterval" /> instance and the specified <paramref name="factor" />.</summary>
        // /// <param name="timeInterval">The value to be multiplied.</param>
        // /// <param name="factor">The value to be multiplied by.</param>
        // /// <returns>A new object that represents the value of the specified <paramref name="timeInterval" /> instance multiplied by the value of the specified <paramref name="factor" />.</returns>
        // public static TimeInterval operator*(TimeInterval timeInterval, double factor) => timeInterval.timeSpan*factor;
        
        // /// <summary>Returns a new <see cref="T:TimeInterval" /> object which value is the result of division of <paramref name="timeInterval" /> instance and the specified <paramref name="divisor" />.</summary>
        // /// <param name="timeInterval">Dividend or the value to be divided.</param>
        // /// <param name="divisor">The value to be divided by.</param>
        // /// <returns>A new object that represents the value of <paramref name="timeInterval" /> instance divided by the value of <paramref name="divisor" />.</returns>
        // public static TimeInterval operator/(TimeInterval timeInterval, double divisor) => timeInterval.timeSpan/divisor;

        // /// <summary>Returns a new <see cref="T:System.Double" /> value which is the result of division of <paramref name="timeInterval" /> instance and the specified <paramref name="timeSpan" />.</summary>
        // /// <param name="timeInterval">Divident or the value to be divided.</param>
        // /// <param name="timeSpan">The value to be divided by.</param>
        // /// <returns>A new value that represents result of division of <paramref name="timeInterval" /> instance by the value of the <paramref name="timeSpan" />.</returns>
        // public static double operator/(TimeInterval timeInterval, TimeSpan timeSpan) => timeInterval.timeSpan/timeSpan;

        #endregion
    }
}