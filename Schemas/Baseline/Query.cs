using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Benchmarks.Schemas.Baseline
{
    public class BaselineQuery : ObjectGraphType
    {
        public BaselineQuery()
        {
            Name = "Query";

            Field<ListGraphType<HumanType>>()
                .Name("humans")
                .Resolve(ctx => ctx.GetDataContext().Humans.ToListAsync());

            Field<ListGraphType<DroidType>>()
                .Name("droids")
                .Resolve(ctx => ctx.GetDataContext().Droids.ToListAsync());

            Field<ListGraphType<EpisodeType>>()
                .Name("episodes")
                .Resolve(ctx => ctx.GetDataContext().Episodes.ToListAsync());
        }
    }
}
