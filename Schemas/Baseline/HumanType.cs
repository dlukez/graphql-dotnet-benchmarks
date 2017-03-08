using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.Baseline
{
    public class HumanType : ObjectGraphType<Human>
    {
        public HumanType()
        {
            Name = "Human";
            Field(h => h.Name);
            Field(h => h.HumanId);
            Field(h => h.HomePlanet);
            Interface<CharacterInterface>();

            Field<ListGraphType<CharacterInterface>>()
                .Name("friends")
                .Resolve(ctx =>
                {
                    var db = ctx.GetDataContext();
                    return Task.FromResult(db.Friendships.Where(f => ctx.Source.HumanId == f.HumanId).Select(f => (ICharacter)f.Droid).ToList());
                });

            Field<ListGraphType<EpisodeType>>()
                .Name("appearsIn")
                .Resolve(ctx =>
                {
                    var db = ctx.GetDataContext();
                    return Task.FromResult(db.HumanAppearances.Where(ha => ctx.Source.HumanId == ha.HumanId).Select(ha => ha.Episode).ToList());
                });
        }
    }
}
