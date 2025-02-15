using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public partial class WorldGenerator(IEnumerable<IWorldGenerator> generators, WorldGenContext context)
    {
        private readonly List<IWorldGenerator> _generators = generators.ToList();
        private readonly WorldGenContext _context = context;

        private readonly TileType[,] _tiles = new TileType[context.Definitions.World.Size.X, context.Definitions.World.Size.Y];

        public event Action<WorldGenProgressInfo> ProgressUpdated;
        public event Action<TileType[,], WorldGenPass> PassCompleted;
        public event Action<TileType[,]> GenerationCompleted;

        public async Task Generate()
        {
            foreach (var generator in _generators)
            {
                generator.ProgressUpdated += UpdateProgress;

                await generator.Generate(_tiles, _context);

                PassCompleted?.Invoke(_tiles, generator.Pass);
            }

            GenerationCompleted?.Invoke(_tiles);
        }

        private void UpdateProgress(WorldGenProgressInfo info)
        {
            var totalProgress = ((int)info.Pass + info.LocalProgress) / _generators.Count;

            var progressInfo = new WorldGenProgressInfo(info.Pass, info.LocalProgress, totalProgress);
            ProgressUpdated?.Invoke(progressInfo);
        }
    }
}
