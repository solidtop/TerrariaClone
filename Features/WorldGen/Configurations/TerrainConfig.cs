using TerrariaClone.Common.Utilities;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class TerrainConfig : ConfigLoader<TerrainConfig>
    {
        public NoiseConfig HeightNoise { get; set; }
        public SplineConfig HeightSpline { get; set; }
        public NoiseConfig StoneOffsetNoise { get; set; }
    }
}
