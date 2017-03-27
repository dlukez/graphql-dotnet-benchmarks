using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.DataLoader
{
    public class DataLoaderQuery : ObjectGraphType
    {
        public DataLoaderQuery()
        {
            Name = "Query";

            Field<ListGraphType<HumanType>>()
                .Name("humans")
                .Resolve(ctx => Task.FromResult(ctx.GetDataContext().Humans.ToList()));

            Field<ListGraphType<DroidType>>()
                .Name("droids")
                .Resolve(ctx => Task.FromResult(ctx.GetDataContext().Droids.ToList()));

            Field<ListGraphType<EpisodeType>>()
                .Name("episodes")
                .Resolve(ctx => Task.FromResult(ctx.GetDataContext().Episodes.ToList()));
        }
    }
}
