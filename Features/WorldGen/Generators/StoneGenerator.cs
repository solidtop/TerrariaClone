using System.Threading.Tasks;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class StoneGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.Stone;
        public override int ChunkSize => 64;

        public override async Task Generate(TileType[,] tiles, WorldGenContext context)
        {
            var worldSize = context.Definitions.World.Size;

            var noise = CreateNoise(context.Seed, context.Config.Stone.Noise);
            var threshold = context.Config.Stone.Noise.Threshold;

            for (int x = 0; x < worldSize.X; x++)
            {
                for (int y = 0; y < worldSize.Y; y++)
                {
                    if (tiles[x, y] != TileType.Dirt)
                        continue;

                    var noiseValue = noise.Sample2D(x, y);

                    if (noiseValue > threshold)
                    {
                        tiles[x, y] = TileType.Stone;
                    }
                }

                await Yield(x, ChunkSize);

                var progress = (float)(x + 1) / worldSize.X;
                UpdateProgress(progress);
            }
        }
    }
}
