using System.Threading.Tasks;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class DirtGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.Dirt;
        public override int ChunkSize => 64;

        public override async Task Generate(TileType[,] tiles, WorldGenContext context)
        {
            var worldSize = context.Definitions.World.Size;
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            var noise = CreateNoise(context.Seed, context.Config.Dirt.Noise);
            var threshold = context.Config.Dirt.Noise.Threshold;

            for (int x = 0; x < worldSize.X; x++)
            {
                for (int y = 0; y < worldSize.Y; y++)
                {
                    if (y < undergroundLevel)
                        continue;

                    var noiseValue = noise.Sample2D(x, y);

                    if (noiseValue > threshold)
                    {
                        tiles[x, y] = TileType.Dirt;
                    }
                }

                await Yield(x, ChunkSize);

                var progress = (float)(x + 1) / worldSize.X;
                UpdateProgress(progress);
            }
        }
    }
}
