using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public interface IWorldGenerator
    {
        WorldGenPass Pass { get; }
        void Generate(TileType[,] tiles, TileRegion region, WorldGenContext context);
    }
}
