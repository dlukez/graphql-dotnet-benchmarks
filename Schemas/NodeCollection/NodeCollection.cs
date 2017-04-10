using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public interface INodeCollection<out T> : IEnumerable<IGraphNode<T>>
    {
        IEnumerable<T> Unwrap();
        Dictionary<TKey, INodeCollection<TResult>> Query<TKey, TResult>(object queryKey, Func<T, TKey> sourceKeySelector, Func<IEnumerable<TKey>, ILookup<TKey, TResult>> resolver);
    }

    public class NodeCollection<T> : INodeCollection<T>
    {
        private readonly Dictionary<object, object> _queryResults = new Dictionary<object, object>();
        private readonly IEnumerable<T> _toReturn;
        private readonly IEnumerable<T> _allItems;

        public NodeCollection(IEnumerable<T> items) : this(items, items)
        {
        }

        public NodeCollection(IEnumerable<T> itemsToReturn, IEnumerable<T> allItems)
        {
            _toReturn = itemsToReturn;
            _allItems = allItems;
        }

        public IEnumerable<T> Unwrap() => _toReturn;

        public Dictionary<TKey, INodeCollection<TResult>> Query<TKey, TResult>(object queryKey, Func<T, TKey> sourceKeySelector, Func<IEnumerable<TKey>, ILookup<TKey, TResult>> resolver)
        {
            object result;
            if (_queryResults.TryGetValue(queryKey, out result))
                return (Dictionary<TKey, INodeCollection<TResult>>)result;

            var sourceKeys = _allItems.OfType<T>().Select(sourceKeySelector).ToArray();
            var groupedResult = resolver(sourceKeys);
            var flatResult = groupedResult.SelectMany(x => x).ToArray();
            var joinedResult = sourceKeys.ToDictionary(x => x, key => (INodeCollection<TResult>)new NodeCollection<TResult>(groupedResult[key], flatResult));
            _queryResults.Add(queryKey, joinedResult);
            return joinedResult;
        }

        public IEnumerator<IGraphNode<T>> GetEnumerator()
        {
            foreach (var item in _toReturn)
                yield return new GraphNode<T>(item, this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}