using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public class NodeCollectionSchema : Schema
    {
        public NodeCollectionSchema()
        {
            Query = new NodeCollectionQuery();
        }
    }
}