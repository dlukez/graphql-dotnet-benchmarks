using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.BatchResolver
{
    public class BatchResolverSchema : Schema
    {
        public BatchResolverSchema()
        {
            Query = new BatchResolverQuery();
        }
    }
}