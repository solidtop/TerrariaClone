using Godot;
using TerrariaClone.Features.WorldStreaming.Chunks;

namespace TerrariaClone.Features.WorldStreaming.Pipeline
{
    public class WorldStreamingPipeline(WorldStreamer worldStreamer)
    {
        private readonly WorldStreamer _worldStreamer = worldStreamer;

        public IChunkStreamer ChunkStreamer => _worldStreamer.ChunkStreamer;

        public void Run(Node rootNode)
        {
            rootNode.AddChild(_worldStreamer);
        }
    }
}
