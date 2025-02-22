using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class SpawnPointInitializer : IWorldInitializer
    {
        public Task InitializeAsync(WorldGenContext context, WorldGenState state)
        {
            var worldWidth = context.Definitions.World.Size.X;
            var worldHeight = context.Definitions.World.Size.Y;

            var spawnX = worldWidth / 2;
            var spawnY = (state.HeightMap[spawnX] - 4) * Tile.Size; //(state.HeightMap[worldWidth] - 4);// * Tile.Size;

            state.PlayerSpawnPoint = new(spawnX, spawnY);

            GD.Print("Player spawn point set");

            return Task.CompletedTask;
        }
    }
}
