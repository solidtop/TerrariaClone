using TerrariaClone.Common.Utilities;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class LargeCaveConfig : ConfigLoader<LargeCaveConfig>
    {
        public NoiseConfig CaveNoise { get; set; }
        public NoiseConfig OffsetLevelNoise { get; set; }
    }
}
