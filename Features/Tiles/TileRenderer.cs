using System.Collections.Generic;
using Godot;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Generators;

namespace TerrariaClone.Features.Tiles
{
    public partial class TileRenderer(WorldGenerator worldGenerator) : Node
    {
        private readonly WorldGenerator _worldGenerator = worldGenerator;

        private TileMapLayer _terrainLayer;
        private BetterTerrain _terrain;

        private readonly Queue<Godot.Collections.Dictionary<Vector2I, int>> _cellDataQueue = [];

        public override void _Ready()
        {
            _terrainLayer = GetNode<TileMapLayer>("../TileMapLayer");
            _terrain = new(_terrainLayer);

            _worldGenerator.GenerationCompleted += OnGenerationCompleted;
        }

        public override void _Process(double delta)
        {
            if (_cellDataQueue.TryDequeue(out var cellData))
            {
                GD.Print("Creating changeset...");
                var changeset = _terrain.CreateTerrainChangeset(cellData);
            }
        }

        private void OnGenerationCompleted(TileType[,] tiles)
        {
            var worldSize = new Vector2I(tiles.GetLength(0), tiles.GetLength(1));
            var regions = PartitionWorld(worldSize, new(175, 900));

            foreach (var region in regions)
            {
                var cellData = new Godot.Collections.Dictionary<Vector2I, int>();

                for (int x = region.Start.X; x < region.End.X; x++)
                {
                    for (int y = region.Start.Y; y < region.End.Y; y++)
                    {
                        var tile = tiles[x, y];

                        if (tile == TileType.Air)
                            continue;

                        var cellCoord = new Vector2I(x, y);
                        var tileId = Tile.GetMetadata(tile).Id;
                        cellData[cellCoord] = tileId;
                    }
                }

                _cellDataQueue.Enqueue(cellData);
            }

            //var cellData = new Dictionary<Vector2I, int>();

            //var worldWidth = tiles.GetLength(0);
            //var worldHeight = tiles.GetLength(1);

            //for (int x = 0; x < worldWidth; x++)
            //{
            //    for (int y = 0; y < worldHeight; y++)
            //    {
            //        var tile = tiles[x, y];

            //        if (tile == TileType.Air)
            //            continue;

            //        var cellCoord = new Vector2I(x, y);
            //        var tileId = Tile.GetMetadata(tile).Id;
            //        cellData[cellCoord] = tileId;
            //    }
            //}

            //_changeset = _terrain.CreateTerrainChangeset(cellData);
        }

        private List<WorldRegion> PartitionWorld(Vector2I worldSize, Vector2I regionSize)
        {
            var regions = new List<WorldRegion>();

            for (int x = 0; x < worldSize.X; x += regionSize.X)
            {
                for (int y = 0; y < worldSize.Y; y += regionSize.Y)
                {
                    var region = new WorldRegion(new(x, y), regionSize);
                    regions.Add(region);
                }
            }

            return regions;
        }
    }
}
