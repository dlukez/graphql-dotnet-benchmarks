using System.Linq;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public class DroidType : ObjectGraphType<GraphNode<Droid>>
    {
        public DroidType()
        {
            Name = "Droid";

            Field(d => d.Data.Name);
            Field(d => d.Data.DroidId);
            Field(d => d.Data.PrimaryFunction);
            Interface<CharacterInterface>();

            Field<ListGraphType<CharacterInterface>>("friends",
                resolve: ctx => ctx.Source.Collection.Query(ctx.FieldDefinition, d => d.DroidId, ids =>
                    ctx.GetDataContext()
                       .Friendships
                       .Where(d => ids.Contains(d.DroidId))
                       .ToLookup(f => f.DroidId, f => f.Human)
                    )[ctx.Source.Data.DroidId]);

            Field<ListGraphType<EpisodeType>>("appearsIn",
                resolve: ctx => ctx.Source.Collection.Query(ctx.FieldDefinition, d => d.DroidId, ids =>
                    ctx.GetDataContext()
                       .DroidAppearances
                       .Where(da => ids.Contains(da.DroidId))
                       .ToLookup(da => da.DroidId, da => da.Episode)
                    )[ctx.Source.Data.DroidId]);
        }
    }
}
