using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.BatchResolver
{
    public class CharacterInterface : InterfaceGraphType<ICharacter>
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
