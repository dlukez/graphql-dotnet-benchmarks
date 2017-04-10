namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public interface IGraphNode<out T>
    {
        T Data { get; }

        INodeCollection<T> Collection { get; }
    }

    public class GraphNode<T> : IGraphNode<T>
    {
        public T Data { get; }

        public INodeCollection<T> Collection { get; }

        public GraphNode(T data, INodeCollection<T> collection)
        {
            Data = data;
            Collection = collection;
        }
    }
}