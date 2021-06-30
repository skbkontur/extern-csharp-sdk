using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Client.Primitives
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Progress
    {
        public double CurrentValue { get; }
        public double MaxValue { get; }
        public double Percent { get; }
    }
}