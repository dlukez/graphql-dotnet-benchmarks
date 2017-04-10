using System.Linq;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public class HumanType : ObjectGraphType<GraphNode<Human>>
    {
        public HumanType()
        {
            Name = "Human";

            Field(d => d.Data.Name);
            Field(d => d.Data.HumanId);
            Field(d => d.Data.HomePlanet);
            Interface<CharacterInterface>();

            Field<ListGraphType<CharacterInterface>>("friends",
                resolve: ctx =>
                    ctx.Source.Collection.Query(ctx.FieldDefinition, d => d.HumanId, ids => 
                        ctx.GetDataContext()
                           .Friendships
                           .Where(d => ids.Contains(d.HumanId))
                           .ToLookup(f => f.HumanId, f => f.Human)
                    )[ctx.Source.Data.HumanId]);

            Field<ListGraphType<EpisodeType>>("appearsIn",
                resolve: ctx =>
                    ctx.Source.Collection.Query(ctx.FieldDefinition, d => d.HumanId, ids =>
                        ctx.GetDataContext()
                           .HumanAppearances
                           .Where(da => ids.Contains(da.HumanId))
                           .ToLookup(da => da.HumanId, da => da.Episode)
                    )[ctx.Source.Data.HumanId]);
        }
    }
}