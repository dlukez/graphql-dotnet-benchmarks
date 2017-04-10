using System.Linq;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public class NodeCollectionQuery : ObjectGraphType
    {
        public NodeCollectionQuery()
        {
            Name = "Query";

            Field<ListGraphType<HumanType>>()
                .Name("humans")
                .Resolve(ctx => new NodeCollection<Human>(ctx.GetDataContext().Humans.ToList()));

            Field<ListGraphType<DroidType>>()
                .Name("droids")
                .Resolve(ctx => new NodeCollection<Droid>(ctx.GetDataContext().Droids.ToList()));

            Field<ListGraphType<EpisodeType>>()
                .Name("episodes")
                .Resolve(ctx => new NodeCollection<Episode>(ctx.GetDataContext().Episodes.ToList()));
        }
    }
}
