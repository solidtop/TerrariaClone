using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.WorldStreaming.Chunks;

namespace TerrariaClone.Features.WorldGen.State
{
    public class WorldGenState(Vector2I worldSize)
    {
        public BlockType[,] Blocks { get; set; } = new BlockType[worldSize.X, worldSize.Y];
        public int[] HeightMap { get; set; } = new int[worldSize.X];

        public List<Chunk> Chunks { get; set; }
        public Vector2I PlayerSpawnPoint { get; set; }
    }
}
