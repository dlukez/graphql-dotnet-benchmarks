using System.Linq;
using System.Threading.Tasks;
using GraphQL.BatchResolver;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Benchmarks.Schemas.BatchResolver
{
    public class DroidType : ObjectGraphType<Droid>
    {
        public DroidType()
        {
            Name = "Droid";
            Field(d => d.Name);
            Field(d => d.DroidId);
            Field(d => d.PrimaryFunction, nullable: true);

            Field<ListGraphType<CharacterInterface>>()
                .Name("friends")
                .Batch(d => d.DroidId)
                .Resolve(async ctx =>
                {
                    var ids = ctx.Source;
                    var db = ctx.GetDataContext();
                    return (await db.Friendships
                            .Where(f => ids.Contains(f.DroidId))
                            .Include(f => f.Human)
                            .ToListAsync())
                            .ToLookup(x => x.DroidId, x => (ICharacter)x.Human);
                });

            Field<ListGraphType<EpisodeType>>()
                .Name("appearsIn")
                .Batch(d => d.DroidId)
                .Resolve(async ctx =>
                {
                    var ids = ctx.Source;
                    var db = ctx.GetDataContext();
                    return (await db.DroidAppearances
                            .Where(da => ids.Contains(da.DroidId))
                            .Include(da => da.Episode)
                            .ToListAsync())
                            .ToLookup(x => x.DroidId, x => x.Episode);
                });
                
            Interface<CharacterInterface>();
        }
    }
}
