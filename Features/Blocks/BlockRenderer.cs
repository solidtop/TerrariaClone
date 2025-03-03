using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.WorldStreaming.Chunks;

namespace TerrariaClone.Features.Blocks
{
    public partial class BlockRenderer(IChunkStreamer chunkStreamer) : Node
    {
        private readonly IChunkStreamer _chunkStreamer = chunkStreamer;
        private TileMapLayer _terrainLayer;

        public override void _Ready()
        {
            _terrainLayer = GetNode<TileMapLayer>("../TileMapLayer");

            _chunkStreamer.ChunksLoaded += OnChunksLoaded;
            _chunkStreamer.ChunksUnloaded += OnChunksUnloaded;
        }

        private void OnChunksLoaded(List<Chunk> newChunks)
        {
            Render(newChunks);
        }

        private void OnChunksUnloaded(List<Chunk> oldChunks)
        {
            Render(oldChunks, remove: true);
        }

        private void Render(List<Chunk> chunks, bool remove = false)
        {
            foreach (var chunk in chunks)
            {
                var chunkWorldPos = chunk.Position * chunk.Size;

                for (int x = 0; x < chunk.Size.X; x++)
                {
                    for (int y = 0; y < chunk.Size.Y; y++)
                    {
                        var block = chunk.Blocks[x, y];

                        if (block == BlockType.Air)
                            continue;

                        var cellCoords = chunkWorldPos + new Vector2I(x, y);

                        if (remove)
                        {
                            _terrainLayer.EraseCell(cellCoords);
                            continue;
                        }

                        var atlasCoords = Block.GetMetadata(block).AtlasCoords;
                        _terrainLayer.SetCell(cellCoords, 0, atlasCoords);
                    }
                }
            }
        }
    }
}
