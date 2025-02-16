using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class StoneGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.Stone;

        private PerlinNoise _noise;

        public override void Generate(TileType[,] tiles, TileRegion region, WorldGenContext context)
        {
            _noise ??= CreateNoise(context.Seed, context.Config.Stone.Noise);
            var threshold = context.Config.Stone.Noise.Threshold;

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                for (int y = region.Start.Y; y < region.End.Y; y++)
                {
                    if (tiles[x, y] != TileType.Dirt)
                        continue;

                    var noiseValue = _noise.Sample2D(x, y);

                    if (noiseValue > threshold)
                    {
                        tiles[x, y] = TileType.Stone;
                    }
                }
            }
        }
    }
}
