using System.Diagnostics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Kontur.Extern.Client.Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IConfig? config = null;
            if (Debugger.IsAttached)
            {
                config = new DebugInProcessConfig();
            }
            
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
            //BenchmarkRunner.Run(typeof(Program).Assembly, config, args);
        }
    }
}