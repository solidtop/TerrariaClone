using System.IO;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Chunks.Persistence;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class SaveInitializer(IChunkWriter chunkWriter) : IWorldInitializer
    {
        private readonly IChunkWriter _chunkWriter = chunkWriter;

        public Task InitializeAsync(WorldGenContext context, WorldGenState state)
        {
            foreach (var chunk in state.Chunks)
            {
                var filePath = GetFilePath(chunk.Position);
                _chunkWriter.Write(chunk, filePath);
            }

            GD.Print("World saved");

            return Task.CompletedTask;
        }

        private static string GetFilePath(Vector2I position)
        {
            return Path.Combine("user://", $"chunk_{position.X}_{position.Y}.save");
        }
    }
}
