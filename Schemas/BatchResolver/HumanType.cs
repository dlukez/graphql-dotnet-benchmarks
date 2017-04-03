using System.Linq;
using System.Threading.Tasks;
using GraphQL.BatchResolver;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Benchmarks.Schemas.BatchResolver
{
    public class HumanType : ObjectGraphType<Human>
    {
        public HumanType()
        {
            Name = "Human";
            Field(h => h.Name);
            Field(h => h.HumanId);
            Field(h => h.HomePlanet);

            Field<ListGraphType<CharacterInterface>>()
                .Name("friends")
                .Batch(h => h.HumanId)
                .Resolve(async ctx =>
                {
                    var ids = ctx.Source;
                    var db = ctx.GetDataContext();
                    return (await db.Friendships
                            .Where(f => ids.Contains(f.HumanId))
                            .Include(f => f.Droid)
                            .ToListAsync())
                            .ToLookup(f => f.HumanId, f => (ICharacter)f.Droid);
                });

            Field<ListGraphType<EpisodeType>>()
                .Name("appearsIn")
                .Batch(h => h.HumanId)
                .Resolve(async ctx =>
                {
                    var ids = ctx.Source;
                    var db = ctx.GetDataContext();
                    return (await db.HumanAppearances
                            .Where(ha => ids.Contains(ha.HumanId))
                            .Include(ha => ha.Episode)
                            .ToListAsync())
                            .ToLookup(x => x.HumanId, x => x.Episode);
                });

            Interface<CharacterInterface>();
        }
    }
}
