using System.Linq;
using GraphQL.BatchResolver;
using GraphQL.BatchResolver.TaskExtensions;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

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
                .Resolve(ctx => ctx.GetDataContext().Humans.ToListAsync().AsEnumerable());

            Field<ListGraphType<DroidType>>()
                .Name("droids")
                .Batch()
                .Resolve(ctx => ctx.GetDataContext().Droids.ToListAsync().AsEnumerable());

            Field<ListGraphType<EpisodeType>>()
                .Name("episodes")
                .Batch()
                .Resolve(ctx => ctx.GetDataContext().Episodes.ToListAsync().AsEnumerable());
        }
    }
}
