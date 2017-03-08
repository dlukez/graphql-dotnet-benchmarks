using DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Benchmarks
{
    public class UserContext
    {
        public StarWarsContext DataContext { get; set; } = new StarWarsContext();
        public DataLoaderContext LoadContext { get; set; }
    }

    public static class UserContextExtensions
    {
        public static StarWarsContext GetDataContext<T>(this ResolveFieldContext<T> context)
        {
            return ((UserContext)context.UserContext).DataContext;
        }

        public static IDataLoader<int, TReturn> GetDataLoader<TSource, TReturn>(this ResolveFieldContext<TSource> context, Func<IEnumerable<int>, Task<ILookup<int, TReturn>>> fetchDelegate)
        {
            return ((UserContext)context.UserContext).LoadContext.GetLoader(context.FieldDefinition, fetchDelegate);
        }
    }
}
