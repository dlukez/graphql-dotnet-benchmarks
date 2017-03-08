using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.Baseline
{
    public class EpisodeType : ObjectGraphType<Episode>
    {
        public EpisodeType()
        {
            Name = "Episode";

            Field("id", e => e.EpisodeId);
            Field("name", e => e.Name);

            Field<ListGraphType<CharacterInterface>>()
                .Name("characters")
                .Resolve(ctx =>
                {
                    var db = ctx.GetDataContext();
                    var humans = db.HumanAppearances.Where(ha => ctx.Source.EpisodeId == ha.EpisodeId).Select(ha => ha.Human).ToList();
                    var droids = db.DroidAppearances.Where(da => ctx.Source.EpisodeId == da.EpisodeId).Select(da => da.Droid).ToList();
                    return Task.FromResult(humans.Concat<ICharacter>(droids));
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