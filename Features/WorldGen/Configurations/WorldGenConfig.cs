using System.IO;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class WorldGenConfig
    {
        public TerrainConfig Terrain {  get; init; }
        public DirtConfig Dirt {  get; init; }
        public StoneConfig Stone {  get; init; }

        public static WorldGenConfig Load(string path)
        {
            var terrainConfig = TerrainConfig.Load(Path.Combine(path, "Terrain.json"));
            var dirtConfig = DirtConfig.Load(Path.Combine(path, "Dirt.json"));
            var stoneConfig = StoneConfig.Load(Path.Combine(path, "Stone.json"));

            return new WorldGenConfig()
            {
                Terrain = terrainConfig,
                Dirt = dirtConfig,
                Stone = stoneConfig,
            };
        }
    }
}
