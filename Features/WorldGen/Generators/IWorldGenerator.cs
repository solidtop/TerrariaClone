using System;
using System.Threading.Tasks;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public interface IWorldGenerator
    {
        event Action<GenerationProgressInfo> ProgressUpdated;
        GenerationPass Pass { get; }
        Task Generate(TileType[,] tiles, WorldGenContext context);
    }
}
