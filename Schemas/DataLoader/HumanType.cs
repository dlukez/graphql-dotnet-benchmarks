using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Benchmarks.Schemas.DataLoader
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
                .Resolve(ctx => ctx.GetDataLoader(ids =>
                    {
                        var db = ctx.GetDataContext();
                        return Task.FromResult(db.Friendships
                            .Where(f => ids.Contains(f.HumanId))
                            .Include(f => f.Droid)
                            .ToLookup(x => x.HumanId, x => (ICharacter)x.Droid));
                    }).LoadAsync(ctx.Source.HumanId));

            Field<ListGraphType<EpisodeType>>()
                .Name("appearsIn")
                .Resolve(ctx => ctx.GetDataLoader(ids =>
                    {
                        var db = ctx.GetDataContext();
                        return Task.FromResult(db.HumanAppearances
                            .Where(ha => ids.Contains(ha.HumanId))
                            .Include(ha => ha.Episode)
                            .ToLookup(x => x.HumanId, x => x.Episode));
                    }).LoadAsync(ctx.Source.HumanId));
        }
    }
}
