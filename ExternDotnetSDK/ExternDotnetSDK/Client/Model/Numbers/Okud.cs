using Kontur.Extern.Client.ApiLevel.Clients.Exceptions;

namespace Kontur.Extern.Client.Model.Numbers
{
    /// <summary>
    /// ОКУД – общероссийский классификатор управленческой документации. Формат данных: семизначный цифровой код
    /// </summary>
    public record Okud
    {
        public static readonly RegexBasedParser<Okud> Parser = 
            new(@"^\d{7}$", v => new Okud(v), (param, value) => Errors.InvalidAuthorityNumber(param, value, AuthorityNumberKind.Okud, "ХХХХХХХ"));

        /// <summary>
        /// Формат данных: семизначный цифровой код
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Okud Parse(string value) => Parser.Parse(value);
        
        private Okud(string value) => Value = value;

        public string Value { get; }
        public AuthorityNumberKind Kind => AuthorityNumberKind.Okud;
    }
}