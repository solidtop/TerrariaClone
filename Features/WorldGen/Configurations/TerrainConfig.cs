using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class TerrainConfig : ConfigLoader<TerrainConfig>
    {
        public TileType DefaultTile { get; set; } = TileType.Stone;
        public TileType DefaultFluid { get; set; } = TileType.Water;
        public NoiseConfig HeightNoise { get; set; }
        public SplineConfig HeightSpline { get; set; }
        public NoiseConfig StoneOffsetNoise { get; set; }
    }
}
