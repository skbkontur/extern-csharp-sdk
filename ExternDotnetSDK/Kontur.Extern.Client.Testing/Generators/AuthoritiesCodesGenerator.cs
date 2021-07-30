using System.Linq;
using JetBrains.Annotations;
using Kontur.Extern.Client.Model.Numbers;

namespace Kontur.Extern.Client.Testing.Generators
{
    public class AuthoritiesCodesGenerator
    {
        private readonly Randomizer randomizer = new();

        [UsedImplicitly]
        public AuthoritiesCodesGenerator()
        {
        }

        public LegalEntityInn LegalEntityInn()
        {
            var regionCode = randomizer.Int(1, 99);
            var vec = new[] {2, 4, 10, 3, 5, 9, 4, 6, 8};
            var n19 = $"{regionCode % 100:00}{randomizer.Int(0, 9999999):0000000}";
            var sum = n19.Select((t, i) => int.Parse(t.ToString())*vec[i]).Sum();
            return Model.Numbers.LegalEntityInn.Parse($"{n19}{sum%11%10}");
        }

        public Kpp Kpp()
        {
            var authorityCode = $"{randomizer.DigitsString(2, 0)}{randomizer.DigitsString(2)}";
            return Model.Numbers.Kpp.Parse($"{authorityCode}50{randomizer.DigitsString(3)}");
        }

        public Inn PersonInn()
        {
            var regionCode = randomizer.Int(1, 99);
            var vec1 = new[] {7, 2, 4, 10, 3, 5, 9, 4, 6, 8};
            var vec2 = new[] {3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8};
            var mainPartOfInn = $"{regionCode % 100:00}{randomizer.Int(0, 99999999):00000000}";
            
            var controlSum = 0;
            for (var i = 0; i < mainPartOfInn.Length; i++)
            {
                controlSum += int.Parse(mainPartOfInn[i].ToString()) * vec1[i];
            }
            var innWithControlSum = $"{mainPartOfInn}{controlSum%11%10}";
            
            var finalControlSum = 0;
            for (var i = 0; i < innWithControlSum.Length; i++)
            {
                finalControlSum += int.Parse(innWithControlSum[i].ToString()) * vec2[i];
            }

            return Inn.Parse($"{innWithControlSum}{finalControlSum%11%10}");
        }
        
        public FssRegNumber FssRegNumber() => Model.Numbers.FssRegNumber.Parse(randomizer.DigitsString(10));
        public PfrRegNumber PfrRegNumber()
        {
            var value = $"{randomizer.DigitsString(3)}-{randomizer.DigitsString(3)}-{randomizer.DigitsString(6)}";
            return Model.Numbers.PfrRegNumber.Parse(value);
        }
    }
}