using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.Baseline
{
    public class CharacterInterface : InterfaceGraphType
    {
        public CharacterInterface()
        {
            Name = "Character";
            Field<StringGraphType>("name", "The name of the character.");
            Field<ListGraphType<CharacterInterface>>("friends");
            Field<ListGraphType<EpisodeType>>("appearsIn");
        }
    }
}
