using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public interface IWorldGenerator
    {
        WorldGenPass Pass { get; }
        void Generate(WorldGenContext context, WorldGenState state, WorldRegion region);
    }
}
