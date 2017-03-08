using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.BatchResolver;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.BatchResolver
{
    public class BatchResolverQuery : ObjectGraphType
    {
        public BatchResolverQuery()
        {
            Name = "Query";

            Field<ListGraphType<HumanType>>()
                .Name("humans")
                .Batch()
                .Resolve(ctx => Task.FromResult((IEnumerable<Human>)ctx.GetDataContext().Humans.ToList()));

            Field<ListGraphType<DroidType>>()
                .Name("droids")
                .Batch()
                .Resolve(ctx => Task.FromResult((IEnumerable<Droid>)ctx.GetDataContext().Droids.ToList()));

            Field<ListGraphType<EpisodeType>>()
                .Name("episodes")
                .Batch()
                .Resolve(ctx => Task.FromResult((IEnumerable<Episode>)ctx.GetDataContext().Episodes.ToList()));
        }
    }
}
