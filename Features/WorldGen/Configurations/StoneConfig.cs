using TerrariaClone.Common.Utilities;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class StoneConfig : ConfigLoader<StoneConfig>
    {
        public NoiseConfig Noise {  get; set; }
    }
}
