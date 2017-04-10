using GraphQL.Types;

namespace GraphQL.Benchmarks.Schemas.NodeCollection
{
    public class CharacterInterface : InterfaceGraphType<GraphNode<ICharacter>>
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
