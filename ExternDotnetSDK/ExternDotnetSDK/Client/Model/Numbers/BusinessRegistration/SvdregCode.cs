using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.Numbers.BusinessRegistration
{
    /// <summary>
    /// СВДРЕГ коды, которые могут быть обязательными для регистрации ИП\ЮЛ.
    /// Формат: пятизначный код, первый символ 0 или X, затем следуют 4 цифры. 
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct SvdregCode
    {
        public static readonly RegexBasedParser<SvdregCode> Parser = new(
            @"^(X|0)\d{4}$",
            v => new SvdregCode(v),
            Exceptions.Errors.InvalidSvdregCode
        );

        /// <summary>
        /// Пятизначный код, первый символ 0 или X, затем следуют 4 цифры.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SvdregCode Parse(string value) => Parser.Parse(value);

        private SvdregCode(string code) => Code = code;

        public string Code { get; }

        public override string ToString() => Code ?? string.Empty;

        public bool Equals(SvdregCode other) => Equals(Code, other.Code);

        public override bool Equals(object? obj) => obj is SvdregCode other && Equals(other);

        public override int GetHashCode() => Code?.GetHashCode() ?? 0;

        public static bool operator==(SvdregCode left, SvdregCode right) => left.Equals(right);

        public static bool operator!=(SvdregCode left, SvdregCode right) => !left.Equals(right);

        public static implicit operator SvdregCode(string value) => new(value);
    }
}