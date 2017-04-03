using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using DataLoader;
using GraphQL.Benchmarks.Schemas.Baseline;
using GraphQL.Benchmarks.Schemas.BatchResolver;
using GraphQL.Benchmarks.Schemas.DataLoader;
using System.Threading.Tasks;

namespace GraphQL.Benchmarks
{
    [ShortRunJob]
    [MemoryDiagnoser]
    public class GraphQL_BatchedIOPerformance
    {
        private const string _query = @"
query CharactersByEpisodeQuery {
  episodes {
    id
    name
    characters {
      ...CharacterFields
      appearsIn {
          id
      }
    }
  }
}

fragment CharacterFields on Character {
  __typename
  ...on Human {
    humanId
    name
    homePlanet
  }
  ...on Droid {
    droidId
    name
    primaryFunction
  }
  appearsIn { name }
  friends { name }
}";

        private DocumentExecuter _executer;
        private UserContext _userContext;
        private ExecutionOptions _dataLoaderOptions;
        private ExecutionOptions _batchResolverOptions;
        private ExecutionOptions _baselineOptions;

        public GraphQL_BatchedIOPerformance()
        {
            _executer = new DocumentExecuter();

            _baselineOptions = new ExecutionOptions();
            _baselineOptions.Query = _query;
            _baselineOptions.Schema = new BaselineSchema();

            _dataLoaderOptions = new ExecutionOptions();
            _dataLoaderOptions.Query = _query;
            _dataLoaderOptions.Schema = new DataLoaderSchema();

            _batchResolverOptions = new ExecutionOptions();
            _batchResolverOptions.Query = _query;
            _batchResolverOptions.Schema = new BatchResolverSchema();

            TestDataGenerator.InitializeDb();
        }

        [Setup]
        public void Setup()
        {
            _userContext = new UserContext();
        }

        [Cleanup]
        public void Cleanup()
        {
            _userContext.DataContext.Dispose();
            _userContext = null;
        }

        [Benchmark(Baseline = true)]
        public Task<ExecutionResult> Baseline()
        {
            _baselineOptions.UserContext = _userContext;
            return _executer.ExecuteAsync(_baselineOptions);
        }

        [Benchmark]
        public Task<ExecutionResult> DataLoader()
        {
            _dataLoaderOptions.UserContext = _userContext;
            return DataLoaderContext.Run(loadCtx =>
            {
                _userContext.LoadContext = loadCtx;
                return _executer.ExecuteAsync(_dataLoaderOptions);
            });
        }

        [Benchmark]
        public Task<ExecutionResult> BatchResolver()
        {
            _batchResolverOptions.UserContext = _userContext;
            return _executer.ExecuteAsync(_batchResolverOptions);
        }
    }
}