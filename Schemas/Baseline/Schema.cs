using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.Baseline
{
    public class BaselineSchema : Schema
    {
        public BaselineSchema()
        {
            Query = new BaselineQuery();
        }
    }
}