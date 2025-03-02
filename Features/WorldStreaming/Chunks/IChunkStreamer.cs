using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace TerrariaClone.Features.WorldStreaming.Chunks
{
    public interface IChunkStreamer
    {
        event Action<List<Chunk>> ChunksLoaded;
        event Action<List<Chunk>> ChunksUnloaded;
        Task StreamAsync(Vector2 cameraPosition);
        void UpdateStreamDistance(Vector2I streamDistance);
    }
}
