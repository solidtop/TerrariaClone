using System;
using System.Collections.Generic;
using TerrariaClone.Common.Monitor;
using TerrariaClone.Features.WorldGen.Configurations;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Definitions;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Pipeline
{
    public class WorldGenPipelineBuilder : IWorldGenPipelineBuilder
    {
        private readonly WorldGenOptions _options = new();
        private readonly List<IWorldGenerator> _generators = [];
        private readonly List<IWorldInitializer> _initializers = [];

        public IWorldGenPipelineBuilder Configure(Action<WorldGenOptions> options)
        {
            options?.Invoke(_options);
            return this;
        }

        public IWorldGenPipelineBuilder AddGenerators(IEnumerable<IWorldGenerator> generators)
        {
            _generators.AddRange(generators);
            return this;
        }

        public IWorldGenPipelineBuilder AddInitializers(IEnumerable<IWorldInitializer> initializers)
        {
            _initializers.AddRange(initializers);
            return this;
        }

        public WorldGenPipeline Build()
        {
            var definitions = WorldDefinitions.Load(_options.ConfigPath);
            var config = WorldGenConfig.Load(_options);

            var context = new WorldGenContext(_options.Seed, definitions, config);
            var state = new WorldGenState(definitions.World.Size);

            var progressMonitor = new ProgressMonitor();

            var worldGenerator = new WorldGenerator(_generators, context, state, progressMonitor);
            var worldInitializer = new WorldInitializer(_initializers, context, state, progressMonitor);

            return new WorldGenPipeline(worldGenerator, worldInitializer, progressMonitor);
        }
    }
}
