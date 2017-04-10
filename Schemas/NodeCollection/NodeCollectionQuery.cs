using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.Benchmarks.NodeCollection
{
    public interface INodeCollectionQuery
    {
        IEnumerable Get(object key);
    }

    public interface INodeCollectionQuery<in TSource, out TResult> : INodeCollectionQuery
    {
        IEnumerable<TResult> Get(TSource source);
    }

    public class NodeCollectionQuery<TSource, TKey, TResult> : INodeCollectionQuery<TSource, TResult>
    {
        private Func<TSource, TKey> _sourceKeySelector;
        private Func<TResult, TKey> _resultKeySelector;
        private Func<IEnumerable<TKey>, IEnumerable<TResult>> _resolver;

        public NodeCollectionQuery(Func<TSource, TKey> sourceKeySelector, Func<TResult, TKey> resultKeySelector, Func<IEnumerable<TKey>, IEnumerable<TResult>> resolver)
        {
            _sourceKeySelector = sourceKeySelector;
            _resultKeySelector = resultKeySelector;
            _resolver = resolver;
        }

        protected ILookup<TKey, TResult> Results { get; set; }
        
        public IEnumerable<TResult> Get(TSource source)
        {
            throw new NotImplementedException();
        }

        IEnumerable INodeCollectionQuery.Get(object key)
        {
            return Get((TSource)key);
        }
    }
}