using Godot;
using TerrariaClone.Common.Serialization;

namespace TerrariaClone.Features.WorldStreaming.Chunks.Persistence
{
    public class ChunkWriter(ISerializer serializer) : IChunkWriter
    {
        private readonly ISerializer _serializer = serializer;

        public void Write(Chunk chunk, string path)
        {
            var data = _serializer.Serialize(chunk);
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
            file.StoreBuffer(data);
        }
    }
}
