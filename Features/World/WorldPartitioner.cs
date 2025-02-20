using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.Chunks;
using TerrariaClone.Features.Tiles;

namespace TerrariaClone.Features.World
{
    public class WorldPartitioner : IWorldPartitioner
    {
        public List<Chunk> Partition(TileType[,] tiles, Vector2I chunkSize)
        {
            var chunks = new List<Chunk>();
            var worldWidth = tiles.GetLength(0);
            var worldHeight = tiles.GetLength(1);

            for (int x = 0; x < worldWidth; x += chunkSize.X)
            {
                for (int y = 0; y < worldHeight; y += chunkSize.Y)
                {
                    var chunkTiles = new TileType[chunkSize.X, chunkSize.Y];

                    for (int i = 0; i < chunkSize.X; i++)
                    {
                        for (int j = 0; j < chunkSize.Y; j++)
                        {
                            chunkTiles[i, j] = tiles[x + i, y + j];
                        }
                    }

                    var chunkPosition = new Vector2I(x, y);
                    var chunk = new Chunk(chunkPosition, chunkSize, chunkTiles);
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}
