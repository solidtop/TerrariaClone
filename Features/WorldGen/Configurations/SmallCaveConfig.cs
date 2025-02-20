using TerrariaClone.Common.Utilities;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class SmallCaveConfig : ConfigLoader<SmallCaveConfig>
    {
        public NoiseConfig CaveNoise { get; set; }
        public NoiseConfig OffsetLevelNoise { get; set; }
    }
}
