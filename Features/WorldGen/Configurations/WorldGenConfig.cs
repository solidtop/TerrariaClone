using System.IO;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class WorldGenConfig
    {
        public TerrainConfig Terrain {  get; init; }

        public static WorldGenConfig Load(string path)
        {
            var terrainConfig = TerrainConfig.Load(Path.Combine(path, "Terrain.json"));

            return new WorldGenConfig()
            {
                Terrain = terrainConfig,
            };
        }
    }
}
