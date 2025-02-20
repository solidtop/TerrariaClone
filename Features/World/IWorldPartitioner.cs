using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.Chunks;
using TerrariaClone.Features.Tiles;

namespace TerrariaClone.Features.World
{
    public interface IWorldPartitioner
    {
        List<Chunk> Partition(TileType[,] tiles, Vector2I chunkSize);
    }
}
