using System;
using System.Collections.Generic;
using System.IO;
using TerrariaClone.Features.WorldGen.Configurations;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Debug;
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
        private List<IWorldGenDebugger> _debuggers = [];

        public IWorldGenPipelineBuilder Configure(Action<WorldGenOptions> options)
        {
            options?.Invoke(_options);
            return this;
        }

        public IWorldGenPipelineBuilder AddGenerator(IWorldGenerator generator)
        {
            _generators.Add(generator);
            return this;
        }

        public IWorldGenPipelineBuilder AddGenerators(IEnumerable<IWorldGenerator> generators)
        {
            _generators.AddRange(generators);
            return this;
        }

        public IWorldGenPipelineBuilder AddInitializer(IWorldInitializer initializer)
        {
            _initializers.Add(initializer);
            return this;
        }

        public IWorldGenPipelineBuilder AddInitializers(IEnumerable<IWorldInitializer> initializers)
        {
            _initializers.AddRange(initializers);
            return this;
        }

        public IWorldGenPipelineBuilder AddDebugger(IWorldGenDebugger debugger)
        {
            _debuggers.Add(debugger);
            return this;
        }

        public IWorldGenPipelineBuilder AddDebuggers(IEnumerable<IWorldGenDebugger> debuggers)
        {
            _debuggers.AddRange(debuggers);
            return this;
        }

        public WorldGenPipeline Build()
        {
            var definitions = WorldDefinitions.Load(Path.Combine(_options.ConfigPath));
            var config = WorldGenConfig.Load(_options.ConfigPath);

            var context = new WorldGenContext(_options.Seed, definitions, config);
            var state = new WorldGenState(definitions.World.Size);

            var worldGenerator = new WorldGenerator(_generators, context, state);
            var worldInitializer = new WorldInitializer(_initializers, context, state);

            return new WorldGenPipeline(worldGenerator, worldInitializer, _debuggers);
        }
    }
}
