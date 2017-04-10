using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using GraphQL.Http;

namespace GraphQL.Benchmarks
{
    class Program
    {
        private static IDocumentWriter _writer = new DocumentWriter(true);

        public static void Main()
        {
            // BenchmarkRunner.Run<GraphQL_BatchedIOPerformance>();
            RunTests().Wait();
        }

        private static async Task RunTests()
        {
            TestDataGenerator.InitializeDb();

            if (!Directory.Exists("log"))
                Directory.CreateDirectory("log");

            await RunTest("Baseline", _ => _.Baseline());
            await RunTest("DataLoader", _ => _.DataLoader());
            await RunTest("BatchResolver", _ => _.BatchResolver());
            await RunTest("NodeCollection", _ => _.NodeCollection());
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
                sw.Stop();
                Console.WriteLine($"{name} finished in {sw.ElapsedMilliseconds}ms");

                File.WriteAllText($"log/result_{name}.json", _writer.Write(result));

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
