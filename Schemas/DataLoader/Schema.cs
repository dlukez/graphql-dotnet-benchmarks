using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.DataLoader
{
    public class DataLoaderSchema : Schema
    {
        public DataLoaderSchema()
        {
            Query = new DataLoaderQuery();
        }
    }
}