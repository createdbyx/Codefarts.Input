// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Codefarts.Input.MonoGameSources;

public class Program
{
    private static void Main()
    {
        var summary = BenchmarkRunner.Run<Md5VsSha256>(new DebugBuildConfig());
    }


// private class CustomConfig : ManualConfig
// {
//     public CustomConfig()
//     {
//         AddJob(new Job());
//         AddValidator(JitOptimizationsValidator.DontFailOnError);
//         AddLogger(ConsoleLogger.Default);
//         AddColumnProvider(DefaultColumnProviders.Instance);
//     }
// }
}

public class Md5VsSha256
{
    [Benchmark]
    public void KeyboardPolling()
    {
        var kbSource = new KeyboardSource();

        for (var i = 0; i < 100000; i++)
        {
            var results = kbSource.Poll(false);
        }
    }
}