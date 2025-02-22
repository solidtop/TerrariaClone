using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.Chunks;
using TerrariaClone.Features.Tiles;

namespace TerrariaClone.Features.WorldGen.State
{
    public class WorldGenState(Vector2I worldSize)
    {
        public TileType[,] Tiles { get; set; } = new TileType[worldSize.X, worldSize.Y];
        public int[] HeightMap { get; set; } = new int[worldSize.X];

        public List<Chunk> Chunks { get; set; }
        public Vector2I PlayerSpawnPoint { get; set; }
    }
}
