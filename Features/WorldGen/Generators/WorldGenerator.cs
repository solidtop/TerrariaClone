using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public partial class WorldGenerator
    {
        private readonly List<IWorldGenerator> _generators;
        private readonly WorldGenContext _context;
        private readonly Vector2I _worldSize;
        private readonly Vector2I _regionSize;

        private readonly TileType[,] _tiles;

        public WorldGenerator(IEnumerable<IWorldGenerator> generators, WorldGenContext context)
        {
            _generators = generators.ToList();
            _context = context;
            _worldSize = context.Definitions.World.Size;
            _regionSize = new Vector2I(_worldSize.X / 50, _worldSize.Y / 50);
            _tiles = new TileType[_worldSize.X, _worldSize.Y];
        }

        public event Action<WorldGenPass, int> ProgressUpdated;
        public event Action<TileType[,], WorldGenPass> PassCompleted;
        public event Action<TileType[,]> GenerationCompleted;

        public async Task Generate()
        {
            foreach (var generator in _generators)
            {
                var tasks = new List<Task>();

                for (int x = 0; x < _worldSize.X; x += _regionSize.X)
                {
                    for (int y = 0; y < _worldSize.Y; y += _regionSize.Y)
                    {
                        var region = new TileRegion(new Vector2I(x, y), _regionSize);

                        tasks.Add(Task.Run(() =>
                        {
                            generator.Generate(_tiles, region, _context);
                        }));
                    }
                }

                await Task.WhenAll(tasks);
                PassCompleted?.Invoke(_tiles, generator.Pass);
                UpdateProgress(generator.Pass);
            }

            GenerationCompleted?.Invoke(_tiles);
        }

        private void UpdateProgress(WorldGenPass pass)
        {
            var percentage = Mathf.FloorToInt(((float)pass / _generators.Count) * 100);
            ProgressUpdated?.Invoke(pass, percentage);
        }
    }
}
