using System.Linq;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public class EpisodeType : ObjectGraphType<GraphNode<Episode>>
    {
        public EpisodeType()
        {
            Name = "Episode";

            Field("id", e => e.Data.EpisodeId);
            Field("name", e => e.Data.Name);

            Field<ListGraphType<CharacterInterface>>()
                .Name("characters")
                .Resolve(ctx =>
                {
                    var inter = ctx.Source.Collection.Query(ctx.FieldDefinition, e => e.EpisodeId, ids =>
                    {
                        var db = ctx.GetDataContext();

                        var humans = db.HumanAppearances
                                    .Where(ha => ids.Contains(ha.EpisodeId))
                                    .Include(ha => ha.Human)
                                    .ToList();

                        var droids = db.DroidAppearances
                                    .Where(da => ids.Contains(da.EpisodeId))
                                    .Include(da => da.Droid)
                                    .ToList();

                        var result = humans.Concat<ICharacterAppearance>(droids).ToLookup(a => a.EpisodeId, a => a.Character);
                        return result;
                    })[ctx.Source.Data.EpisodeId];
                    return inter;
                });
        }
    }

    public class EpisodeEnumType : EnumerationGraphType
    {
        public EpisodeEnumType()
        {
            Name = "EpisodeEnum";
            Description = "One of the films in the Star Wars Trilogy.";
            AddValue("NEWHOPE", "Released in 1977.", 4);
            AddValue("EMPIRE", "Released in 1980.", 5);
            AddValue("JEDI", "Released in 1983.", 6);
        }
    }

    public enum Episodes
    {
        NEWHOPE  = 4,
        EMPIRE  = 5,
        JEDI  = 6
    }
}