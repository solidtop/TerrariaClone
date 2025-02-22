using System;
using System.Collections.Generic;
using TerrariaClone.Features.WorldGen.Debug;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;

namespace TerrariaClone.Features.WorldGen.Pipeline
{
    public interface IWorldGenPipelineBuilder
    {
        IWorldGenPipelineBuilder Configure(Action<WorldGenOptions> options);
        IWorldGenPipelineBuilder AddGenerator(IWorldGenerator generator);
        IWorldGenPipelineBuilder AddGenerators(IEnumerable<IWorldGenerator> generators);
        IWorldGenPipelineBuilder AddInitializer(IWorldInitializer initializer);
        IWorldGenPipelineBuilder AddInitializers(IEnumerable<IWorldInitializer> initializers);
        IWorldGenPipelineBuilder AddDebugger(IWorldGenDebugger debugger);
        IWorldGenPipelineBuilder AddDebuggers(IEnumerable<IWorldGenDebugger> debuggers);
        WorldGenPipeline Build();
    }
}
