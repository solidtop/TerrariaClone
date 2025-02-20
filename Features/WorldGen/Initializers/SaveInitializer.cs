using System.IO;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Chunks.Persistence;
using TerrariaClone.Features.WorldGen.Contexts;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class SaveInitializer(IChunkWriter chunkWriter, string basePath) : IWorldInitializer
    {
        private readonly IChunkWriter _chunkWriter = chunkWriter;
        private readonly string _basePath = basePath;

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

        private string GetFilePath(Vector2I position)
        {
            return Path.Combine(_basePath, $"chunk_{position.X}_{position.Y}.save");
        }
    }
}
