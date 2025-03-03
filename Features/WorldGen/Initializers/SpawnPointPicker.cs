using System.Threading.Tasks;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class SpawnPointPicker(string description) : WorldInitializerBase(description)
    {
        public override Task InitializeAsync(WorldGenContext context, WorldGenState state)
        {
            var worldWidth = context.Definitions.World.Size.X;

            var spawnX = worldWidth / 2;
            var spawnY = (state.HeightMap[spawnX] - 4) * Block.Size;

            state.PlayerSpawnPoint = new(spawnX, spawnY);

            return Task.CompletedTask;
        }
    }
}
