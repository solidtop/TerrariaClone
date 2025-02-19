using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.Tiles;

namespace TerrariaClone.Features.Chunks
{
    public interface IChunkPartitioner
    {
        List<Chunk> Partition(TileType[,] tiles, Vector2I chunkSize);
    }
}
