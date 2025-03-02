using System.IO;
using Godot;
using TerrariaClone.Features.WorldGen.Pipeline;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class WorldGenConfig
    {
        public Vector2I RegionSize { get; init; }
        public Vector2I ChunkSize { get; init; }
        public TerrainConfig Terrain { get; init; }
        public DirtConfig Dirt { get; init; }
        public StoneConfig Stone { get; init; }
        public SmallCaveConfig SmallCave { get; init; }
        public LargeCaveConfig LargeCave { get; init; }

        public static WorldGenConfig Load(WorldGenOptions options)
        {
            var path = options.ConfigPath;

            var terrainConfig = TerrainConfig.Load(Path.Combine(path, "Terrain.json"));
            var dirtConfig = DirtConfig.Load(Path.Combine(path, "Dirt.json"));
            var stoneConfig = StoneConfig.Load(Path.Combine(path, "Stone.json"));
            var smallCaveConfig = SmallCaveConfig.Load(Path.Combine(path, "SmallCave.json"));
            var largeCaveConfig = LargeCaveConfig.Load(Path.Combine(path, "LargeCave.json"));

            return new WorldGenConfig()
            {
                RegionSize = options.RegionSize,
                ChunkSize = options.ChunkSize,
                Terrain = terrainConfig,
                Dirt = dirtConfig,
                Stone = stoneConfig,
                SmallCave = smallCaveConfig,
                LargeCave = largeCaveConfig,
            };
        }
    }
}
