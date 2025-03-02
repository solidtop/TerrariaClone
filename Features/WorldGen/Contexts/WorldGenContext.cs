using TerrariaClone.Common.Definitions;
using TerrariaClone.Features.WorldGen.Configurations;

namespace TerrariaClone.Features.WorldGen.Contexts
{
    public class WorldGenContext(int seed, WorldDefinitions definitions, WorldGenConfig config)
    {
        public int Seed { get; } = seed;
        public WorldDefinitions Definitions { get; } = definitions;
        public WorldGenConfig Config { get; } = config;
    }
}
