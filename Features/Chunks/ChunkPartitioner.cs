using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.Tiles;

namespace TerrariaClone.Features.Chunks
{
    public class ChunkPartitioner : IChunkPartitioner
    {
        public List<Chunk> Partition(TileType[,] tiles, Vector2I chunkSize)
        {
            var chunks = new List<Chunk>();
            var tileLengthX = tiles.GetLength(0);
            var tileLengthY = tiles.GetLength(1);

            for (int x = 0; x < tileLengthX; x += chunkSize.X)
            {
                for (int y = 0; y < tileLengthY; y += chunkSize.Y)
                {
                    var chunkTiles = new TileType[chunkSize.X, chunkSize.Y];

                    for (int i = 0; i < chunkSize.X; i++)
                    {
                        for (int j = 0; j < chunkSize.X; j++)
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
