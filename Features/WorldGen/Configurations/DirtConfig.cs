using TerrariaClone.Common.Utilities;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class DirtConfig : ConfigLoader<DirtConfig>
    {
        public NoiseConfig Noise {  get; set; }
    }
}
