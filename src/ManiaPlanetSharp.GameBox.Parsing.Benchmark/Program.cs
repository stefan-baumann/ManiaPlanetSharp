using BenchmarkDotNet.Running;
using System;

namespace ManiaPlanetSharp.GameBox.Parsing.Benchmark
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadKey(true);
        }
    }
}
