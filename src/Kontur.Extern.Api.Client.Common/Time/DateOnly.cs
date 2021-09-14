#nullable enable
using System;
using System.Globalization;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Common.Time
{
#if NETSTANDARD2_0
    /// <summary>
    /// The wrapper around DateTime to represent a date-only type.
    /// Similar to .NET 6 DateOnly type https://github.com/dotnet/runtime/issues/49036. 
    /// </summary>
    [PublicAPI]
    public readonly struct DateOnly : IComparable<DateOnly>, IEquatable<DateOnly>, IFormattable
    {
        /// <summary>
        /// Return the instance of the DateOnly structure representing the minimal possible date can be created.
        /// </summary>
        public static DateOnly MinValue { get; } = new(DateTime.MinValue.Date);

        /// <summary>
        /// Return the instance of the DateOnly structure representing the maximal possible date can be created.
        /// </summary>
        public static DateOnly MaxValue { get; } = new(DateTime.MaxValue.Date);

        /// <summary>
        /// Converts a string that contains string representation of a date to its DateOnly equivalent by using the conventions of the current culture.
        /// </summary>
        /// <param name="s">The string that contains the string to parse.</param>
        /// <returns>An object that is equivalent to the date contained in s.</returns>
        public static DateOnly Parse(string s) => 
            Parse(s, DateTimeFormatInfo.CurrentInfo);

        /// <summary>
        /// Converts a string that contains string representation of a date to its DateOnly equivalent by using culture-specific format information and a formatting style.
        /// </summary>
        /// <param name="s">The string that contains the string to parse.</param>
        /// <param name="provider">An object that supplies culture-specific format information about s.</param>
        /// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in s for the parse operation to succeed, and that defines how to interpret the parsed date. A typical value to specify is None.</param>
        /// <returns>An object that is equivalent to the date contained in s, as specified by provider and styles.</returns>
        public static DateOnly Parse(string s, IFormatProvider? provider, DateTimeStyles style = DateTimeStyles.None) => 
            new(DateTime.Parse(s, provider, style));

        /// <summary>
        /// Converts the specified string representation of a date to its DateOnly equivalent using the specified format.
        /// The format of the string representation must match the specified format exactly or an exception is thrown.
        /// </summary>
        /// <param name="s">A string containing the characters that represent a date to convert.</param>
        /// <param name="format">A string that represent a format specifier that defines the required format of s.</param>
        /// <returns>An object that is equivalent to the date contained in s, as specified by format.</returns>
        public static DateOnly ParseExact(string s, string format) => 
            ParseExact(s, format, DateTimeFormatInfo.CurrentInfo);

        /// <summary>
        /// Converts the specified string representation of a date to its DateOnly equivalent using the specified format, culture-specific format information, and style.
        /// The format of the string representation must match the specified format exactly or an exception is thrown.
        /// </summary>
        /// <param name="s">A string containing the characters that represent a date to convert.</param>
        /// <param name="format">A string containing the characters that represent a format specifier that defines the required format of s.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
        /// <param name="style">A bitwise combination of the enumeration values that provides additional information about s, about style elements that may be present in s, or about the conversion from s to a DateOnly value. A typical value to specify is None.</param>
        /// <returns>An object that is equivalent to the date contained in s, as specified by format, provider, and style.</returns>
        public static DateOnly ParseExact(string s, string format, IFormatProvider? provider, DateTimeStyles style = DateTimeStyles.None) => 
            new(DateTime.ParseExact(s, format, provider, style));

        /// <summary>
        /// Converts the specified span representation of a date to its DateOnly equivalent using the specified array of formats.
        /// The format of the string representation must match at least one of the specified formats exactly or an exception is thrown.
        /// </summary>
        /// <param name="s">A span containing the characters that represent a date to convert.</param>
        /// <param name="formats">An array of allowable formats of s.</param>
        /// <returns>An object that is equivalent to the date contained in s, as specified by format, provider, and style.</returns>
        public static DateOnly ParseExact(string s, string[] formats) => 
            ParseExact(s, formats, DateTimeFormatInfo.CurrentInfo);

        /// <summary>
        /// Converts the specified string representation of a date to its DateOnly equivalent using the specified array of formats, culture-specific format information, and style.
        /// The format of the string representation must match at least one of the specified formats exactly or an exception is thrown.
        /// </summary>
        /// <param name="s">A string containing the characters that represent a date to convert.</param>
        /// <param name="formats">An array of allowable formats of s.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
        /// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of s. A typical value to specify is None.</param>
        /// <returns>An object that is equivalent to the date contained in s, as specified by format, provider, and style.</returns>
        public static DateOnly ParseExact(string s, string[] formats, IFormatProvider? provider, DateTimeStyles style = DateTimeStyles.None) => 
            new(DateTime.ParseExact(s, formats, provider, style));

        /// <summary>
        /// Converts the specified string representation of a date to its DateOnly equivalent and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing the characters representing the date to convert.</param>
        /// <param name="result">When this method returns, contains the DateOnly value equivalent to the date contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s parameter is empty string, or does not contain a valid string representation of a date. This parameter is passed uninitialized.</param>
        /// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string s, out DateOnly result) => 
            TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);

        /// <summary>
        /// Converts the specified string representation of a date to its DateOnly equivalent using the specified array of formats, culture-specific format information, and style. And returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing the characters that represent a date to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
        /// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of s. A typical value to specify is None.</param>
        /// <param name="result">When this method returns, contains the DateOnly value equivalent to the date contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s parameter is empty string, or does not contain a valid string representation of a date. This parameter is passed uninitialized.</param>
        /// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string s, IFormatProvider? provider, DateTimeStyles style, out DateOnly result)
        {
            if (DateTime.TryParse(s, provider, style, out var dateTime))
            {
                result = new(dateTime);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Converts the specified string representation of a date to its DateOnly equivalent using the specified format and style.
        /// The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing the characters representing a date to convert.</param>
        /// <param name="format">The required format of s.</param>
        /// <param name="result">When this method returns, contains the DateOnly value equivalent to the date contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s is empty string, or does not contain a date that correspond to the pattern specified in format. This parameter is passed uninitialized.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParseExact(string s, string format, out DateOnly result) => 
            TryParseExact(s, format, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);

        /// <summary>
        /// Converts the specified span representation of a date to its DateOnly equivalent using the specified format, culture-specific format information, and style.
        /// The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A span containing the characters representing a date to convert.</param>
        /// <param name="format">The required format of s.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
        /// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of s.</param>
        /// <param name="result">When this method returns, contains the DateOnly value equivalent to the date contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s is empty string, or does not contain a date that correspond to the pattern specified in format. This parameter is passed uninitialized.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParseExact(string s, string format, IFormatProvider? provider, DateTimeStyles style, out DateOnly result)
        {
            if (DateTime.TryParseExact(s, format, provider, style, out var dateTime))
            {
                result = new(dateTime);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Converts the specified string of a date to its DateOnly equivalent and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string containing date to parse.</param>
        /// <param name="formats">An array of allowable formats of s.</param>
        /// <param name="result">When this method returns, contains the DateOnly value equivalent to the date contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s parameter is Empty, or does not contain a valid string representation of a date. This parameter is passed uninitialized.</param>
        /// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        public static bool TryParseExact(string s, string[] formats, out DateOnly result) => 
            TryParseExact(s, formats, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);

        /// <summary>
        /// Converts the specified string of a date to its DateOnly equivalent and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">The string containing the date to parse.</param>
        /// <param name="formats">An array of allowable formats of s.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
        /// <param name="style">A bitwise combination of enumeration values that defines how to interpret the parsed date. A typical value to specify is None.</param>
        /// <param name="result">When this method returns, contains the DateOnly value equivalent to the date contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s parameter is Empty, or does not contain a valid string representation of a date. This parameter is passed uninitialized.</param>
        /// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        public static bool TryParseExact(string s, string[] formats, IFormatProvider? provider, DateTimeStyles style, out DateOnly result)
        {
            if (DateTime.TryParseExact(s, formats, provider, style, out var dateTime))
            {
                result = new(dateTime);
                return true;
            }

            result = default;
            return false;
        }
        
        private readonly DateTime date;

        /// <summary>
        /// Initializes a new instance of the DateOnly structure to the specified year, month, and day.
        /// </summary>
        /// <param name="year">The year (1 through 9999).</param>
        /// <param name="month">The month (1 through 12).</param>
        /// <param name="day">The day (1 through the number of days in month).</param>
        public DateOnly(int year, int month, int day)
            : this(new DateTime(year, month, day))
        {
        }

        /// <summary>
        /// Initializes a new instance of the DateOnly structure to the specified year, month, and day for the specified calendar.
        /// </summary>
        /// <param name="year">The year (1 through the number of years in calendar).</param>
        /// <param name="month">The month (1 through the number of months in calendar).</param>
        /// <param name="day">The day (1 through the number of days in month).<paramref name="month"/>.</param>
        /// <param name="calendar">The calendar that is used to interpret year, month, and day.<paramref name="month"/>.</param>
        public DateOnly(int year, int month, int day, Calendar calendar)
            : this(new DateTime(year, month, day, calendar))
        {
        }

        public DateOnly(DateTime dateTime) => date = dateTime.Date;

        /// <summary>
        /// Gets the year component of the date represented by this instance.
        /// </summary>
        public int Year => date.Year;

        /// <summary>
        /// Gets the month component of the date represented by this instance.
        /// </summary>
        public int Month => date.Month;

        /// <summary>
        /// Gets the day component of the date represented by this instance.
        /// </summary>
        public int Day => date.Day;

        /// <summary>
        /// Gets the day of the week represented by this instance.
        /// </summary>
        public DayOfWeek DayOfWeek => date.DayOfWeek;

        /// <summary>
        /// Gets the day of the year represented by this instance.
        /// </summary>
        public int DayOfYear => date.DayOfYear;

        /// <summary>
        /// Returns a new DateOnly that adds the specified number of days to the value of this instance.
        /// </summary>
        /// <param name="value">A number of days. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date represented by this instance and the number of days represented by value.</returns>
        public DateOnly AddDays(int value) => new(date.AddDays(value));

        /// <summary>
        /// Returns a new DateOnly that adds the specified number of months to the value of this instance.
        /// </summary>
        /// <param name="value">A number of months. The months parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date represented by this instance and months.</returns>
        public DateOnly AddMonths(int value) => new(date.AddMonths(value));

        /// <summary>
        /// Returns a new DateOnly that adds the specified number of years to the value of this instance.
        /// </summary>
        /// <param name="value">A number of years. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date represented by this instance and the number of years represented by value.</returns>
        public DateOnly AddYears(int value) => new(date.AddYears(value));

        /// <summary>
        /// Determines whether two specified instances of DateOnly are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left and right represent the same date; otherwise, false.</returns>
        public static bool operator==(DateOnly left, DateOnly right) => left.date == right.date;

        /// <summary>
        /// Determines whether one specified DateOnly is later than another specified DateTime.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left is later than right; otherwise, false.</returns>
        public static bool operator>(DateOnly left, DateOnly right) => left.date > right.date;

        /// <summary>
        /// Determines whether one specified DateOnly represents a date that is the same as or later than another specified DateOnly.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left is the same as or later than right; otherwise, false.</returns>
        public static bool operator>=(DateOnly left, DateOnly right) => left.date >= right.date;

        /// <summary>
        /// Determines whether two specified instances of DateOnly are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left and right do not represent the same date; otherwise, false.</returns>
        public static bool operator!=(DateOnly left, DateOnly right) => left.date != right.date;

        /// <summary>
        /// Determines whether one specified DateOnly is earlier than another specified DateOnly.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left is earlier than right; otherwise, false.</returns>
        public static bool operator<(DateOnly left, DateOnly right) => left.date < right.date;

        /// <summary>
        /// Determines whether one specified DateOnly represents a date that is the same as or earlier than another specified DateOnly.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left is the same as or earlier than right; otherwise, false.</returns>
        public static bool operator<=(DateOnly left, DateOnly right) => left.date <= right.date;

        public static implicit operator DateOnly(DateTime dateTime) => new(dateTime);

        public DateTime AsDateTime() => date;

        /// <summary>
        /// Compares the value of this instance to a specified DateOnly value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified DateTime value.
        /// </summary>
        /// <param name="value">The object to compare to the current instance.</param>
        /// <returns>Less than zero if this instance is earlier than value. Greater than zero if this instance is later than value. Zero if this instance is the same as value.</returns>
        public int CompareTo(DateOnly value) => 
            date.CompareTo(value.date);

        /// <summary>
        /// Compares the value of this instance to a specified object that contains a specified DateOnly value, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified DateOnly value.
        /// </summary>
        /// <param name="value">A boxed object to compare, or null.</param>
        /// <returns>Less than zero if this instance is earlier than value. Greater than zero if this instance is later than value. Zero if this instance is the same as value.</returns>
        public int CompareTo(object? value)
        {
            if (value == null) 
                return 1;
            
            if (!(value is DateOnly dateOnly))
                throw new InvalidOperationException();

            return CompareTo(dateOnly);
        }

        /// <summary>
        /// Returns a value indicating whether the value of this instance is equal to the value of the specified DateOnly instance.
        /// </summary>
        /// <param name="value">The object to compare to this instance.</param>
        /// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
        public bool Equals(DateOnly value) => 
            value.date.Equals(date);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="value">The object to compare to this instance.</param>
        /// <returns>true if value is an instance of DateOnly and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object? value) => 
            value is DateOnly dateOnly && dateOnly.date.Equals(date);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => date.GetHashCode();

        /// <summary>
        /// Converts the value of the current DateOnly object to its equivalent long date string representation.
        /// </summary>
        /// <returns>A string that contains the long date string representation of the current DateOnly object.</returns>
        public string ToLongDateString() => ToString("D");

        /// <summary>
        /// Converts the value of the current DateOnly object to its equivalent short date string representation.
        /// </summary>
        /// <returns>A string that contains the short date string representation of the current DateOnly object.</returns>
        public string ToShortDateString() => ToString();

        /// <summary>
        /// Converts the value of the current DateOnly object to its equivalent string representation using the formatting conventions of the current culture.
        /// The DateOnly object will be formatted in short form.
        /// </summary>
        /// <returns>A string that contains the short date string representation of the current DateOnly object.</returns>
        public override string ToString() => ToString("d");

        /// <summary>
        /// Converts the value of the current DateOnly object to its equivalent string representation using the specified format and the formatting conventions of the current culture.
        /// </summary>
        /// <param name="format">A standard or custom date format string.</param>
        /// <returns>A string representation of value of the current DateOnly object as specified by format.</returns>
        public string ToString(string? format) => ToString(format, DateTimeFormatInfo.CurrentInfo);

        /// <summary>
        /// Converts the value of the current DateOnly object to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of the current DateOnly object as specified by provider.</returns>
        public string ToString(IFormatProvider? provider) => ToString("d", provider);

        /// <summary>
        /// Converts the value of the current DateOnly object to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="format">A standard or custom date format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of the current DateOnly object as specified by format and provider.</returns>
        public string ToString(string? format, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(format))
                format = "d";

            return date.ToString(format, provider);
        }
    }
#endif
}