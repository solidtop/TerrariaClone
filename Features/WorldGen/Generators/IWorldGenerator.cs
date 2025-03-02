using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public interface IWorldGenerator
    {
        string Description { get; }
        void Generate(WorldGenContext context, WorldGenState state, WorldRegion region);
    }
}
