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
                .ResolveCollection(ctx => ctx.GetDataContext().Humans.ToList());

            Field<ListGraphType<DroidType>>()
                .Name("droids")
                .ResolveCollection(ctx => ctx.GetDataContext().Droids.ToList());

            Field<ListGraphType<EpisodeType>>()
                .Name("episodes")
                .ResolveCollection(ctx => ctx.GetDataContext().Episodes.ToList());
        }
    }
}
