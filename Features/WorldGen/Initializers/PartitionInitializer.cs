using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class PartitionInitializer(IWorldPartitioner partitioner) : IWorldInitializer
    {
        private readonly IWorldPartitioner _partitioner = partitioner;

        public Task InitializeAsync(WorldGenContext context, WorldGenState state)
        {
            var chunkSize = context.Config.World.ChunkSize;
            var chunks = _partitioner.Partition(state.Tiles, chunkSize);
            state.Chunks = _partitioner.Partition(state.Tiles, chunkSize);
            GD.Print($"World partitioned into {chunks.Count} chunks.");

            return Task.CompletedTask;
        }
    }
}
