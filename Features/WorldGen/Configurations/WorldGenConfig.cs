using System.IO;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class WorldGenConfig
    {
        public WorldConfig World { get; init; }
        public TerrainConfig Terrain { get; init; }
        public DirtConfig Dirt { get; init; }
        public StoneConfig Stone { get; init; }
        public SmallCaveConfig SmallCave { get; init; }
        public LargeCaveConfig LargeCave { get; init; }

        public static WorldGenConfig Load(string path)
        {
            var worldConfig = WorldConfig.Load(Path.Combine(path, "World.json"));
            var terrainConfig = TerrainConfig.Load(Path.Combine(path, "Terrain.json"));
            var dirtConfig = DirtConfig.Load(Path.Combine(path, "Dirt.json"));
            var stoneConfig = StoneConfig.Load(Path.Combine(path, "Stone.json"));
            var smallCaveConfig = SmallCaveConfig.Load(Path.Combine(path, "SmallCave.json"));
            var largeCaveConfig = LargeCaveConfig.Load(Path.Combine(path, "LargeCave.json"));

            return new WorldGenConfig()
            {
                World = worldConfig,
                Terrain = terrainConfig,
                Dirt = dirtConfig,
                Stone = stoneConfig,
                SmallCave = smallCaveConfig,
                LargeCave = largeCaveConfig,
            };
        }
    }
}
