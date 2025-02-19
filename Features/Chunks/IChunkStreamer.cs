using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace TerrariaClone.Features.Chunks
{
    public interface IChunkStreamer
    {
        event Action<List<Chunk>> ChunksLoaded;
        Task LoadChunksAsync(Vector2 cameraPosition);
        Task UnloadChunksAsync(Vector2 cameraPosition);
        void UpdateStreamDistance(Vector2I streamDistance);
    }
}
