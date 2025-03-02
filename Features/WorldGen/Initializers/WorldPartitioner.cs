using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;
using TerrariaClone.Features.WorldStreaming.Chunks;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class WorldPartitioner(string description) : WorldInitializerBase(description)
    {
        public override Task InitializeAsync(WorldGenContext context, WorldGenState state)
        {
            var chunkSize = context.Config.ChunkSize;
            state.Chunks = Partition(state.Tiles, chunkSize);

            return Task.CompletedTask;
        }

        private static List<Chunk> Partition(TileType[,] tiles, Vector2I chunkSize)
        {
            var chunks = new List<Chunk>();
            var chunkCountX = tiles.GetLength(0) / chunkSize.X;
            var chunkCountY = tiles.GetLength(1) / chunkSize.Y;

            for (int x = 0; x < chunkCountX; x++)
            {
                for (int y = 0; y < chunkCountY; y++)
                {
                    var chunkTiles = new TileType[chunkSize.X, chunkSize.Y];

                    var absoluteX = x * chunkSize.X;
                    var absoluteY = y * chunkSize.Y;

                    for (int i = 0; i < chunkSize.X; i++)
                    {
                        for (int j = 0; j < chunkSize.Y; j++)
                        {
                            chunkTiles[i, j] = tiles[absoluteX + i, absoluteY + j];
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
