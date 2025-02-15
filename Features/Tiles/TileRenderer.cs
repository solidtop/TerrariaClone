using Godot;
using Godot.Collections;
using TerrariaClone.Features.WorldGen.Generators;

namespace TerrariaClone.Features.Tiles
{
    public partial class TileRenderer(WorldGenerator worldGenerator) : Node
    {
        private readonly WorldGenerator _worldGenerator = worldGenerator; 

        private TileMapLayer _terrainLayer;
        private BetterTerrain _terrain;

        private Dictionary<Variant, Variant> _changeset = [];

        public override void _Ready()
        {
            _terrainLayer = GetNode<TileMapLayer>("../TileMapLayer");
            _terrain = new(_terrainLayer);

            _worldGenerator.GenerationCompleted += Render;
        }

        public override void _Process(double delta)
        {
            if (_terrain.IsTerrainChangesetReady(_changeset))
            {
                _terrain.ApplyTerrainChangeset(_changeset);
            }
        }

        private void Render(TileType[,] tiles)
        {
            var cellData = new Dictionary<Vector2I, int>();

            var worldWidth = tiles.GetLength(0);
            var worldHeight = tiles.GetLength(1);

            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    var tile = tiles[x, y];

                    if (tile == TileType.Air)
                        continue;

                    var cellCoord = new Vector2I(x, y);
                    var tileId = Tile.GetMetadata(tile).Id;
                    cellData[cellCoord] = tileId;
                }
            }

            _changeset = _terrain.CreateTerrainChangeset(cellData);
        }
    }
}
