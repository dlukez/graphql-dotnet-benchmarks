//using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using GraphQL.Http;

namespace GraphQL.Benchmarks
{
    class Program
    {
        public static void Main()
        {
            TestDataGenerator.InitializeDb();

            var writer = new DocumentWriter(true);

            var results = new List<ExecutionResult>();

            results.Add(RunTest("Baseline", _ => _.Baseline()).Result);
            results.Add(RunTest("DataLoader", _ => _.DataLoader()).Result);
            results.Add(RunTest("BatchResolver", _ => _.BatchResolver()).Result);

            for (var i = 0; i < results.Count; i++)
            {
                File.WriteAllText("log/result" + i + ".json", writer.Write(results[i]));
            }
        }

        private static async Task<ExecutionResult> RunTest(string name, Func<GraphQL_BatchedIOPerformance, Task<ExecutionResult>> func)
        {
            try
            {
                var harness = new GraphQL_BatchedIOPerformance();
                harness.Setup();

                Console.WriteLine($"Running {name}: ");
                var sw = Stopwatch.StartNew();
                var result = await func(harness);
                Console.WriteLine($"{name} finished in {sw.ElapsedMilliseconds}ms");

                harness.Cleanup();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops, error occurred: {e}");
            }

            return null;
        }
    }
}
