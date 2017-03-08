using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.Baseline
{
    public class DroidType : ObjectGraphType<Droid>
    {
        public DroidType()
        {
            Name = "Droid";
            Field(d => d.Name);
            Field(d => d.DroidId);
            Field(d => d.PrimaryFunction);
            Interface<CharacterInterface>();

            Field<ListGraphType<CharacterInterface>>()
                .Name("friends")
                .Resolve(ctx =>
                {
                    var db = ctx.GetDataContext();
                    return Task.FromResult(db.Friendships.Where(f => f.DroidId == ctx.Source.DroidId).Select(f => (ICharacter)f.Human).ToList());
                });

            Field<ListGraphType<EpisodeType>>()
                .Name("appearsIn")
                .Resolve(ctx =>
                {
                    var db = ctx.GetDataContext();
                    return Task.FromResult(db.DroidAppearances.Where(da => da.DroidId == ctx.Source.DroidId).Select(da => da.Episode).ToList());
                });
        }
    }
}
