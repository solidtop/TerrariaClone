using System.IO;

namespace TerrariaClone.Common.Definitions
{
    public class WorldDefinitions
    {
        public WorldDefinition World { get; init; }

        public static WorldDefinitions Load(string path)
        {
            var world = WorldDefinition.Load(Path.Combine(path, "Definitions", "World.json"));

            return new WorldDefinitions()
            {
                World = world,
            };
        }
    }
}
