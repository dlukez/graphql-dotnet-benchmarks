using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Benchmarks.Schemas.Baseline
{
    public class EpisodeType : ObjectGraphType<Episode>
    {
        public EpisodeType()
        {
            Name = "Episode";

            Field("id", e => e.EpisodeId);
            Field("name", e => e.Name);

            FieldAsync<ListGraphType<CharacterInterface>>("characters", resolve: async ctx =>
            {
                var db = ctx.GetDataContext();
                var humans = await db.HumanAppearances.Where(ha => ctx.Source.EpisodeId == ha.EpisodeId).Select(ha => ha.Human).ToListAsync();
                var droids = await db.DroidAppearances.Where(da => ctx.Source.EpisodeId == da.EpisodeId).Select(da => da.Droid).ToListAsync();
                return humans.Concat<ICharacter>(droids);
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