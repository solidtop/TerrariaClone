using System;
using System.Collections.Generic;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;

namespace TerrariaClone.Features.WorldGen.Pipeline
{
    public interface IWorldGenPipelineBuilder
    {
        IWorldGenPipelineBuilder Configure(Action<WorldGenOptions> options);
        IWorldGenPipelineBuilder AddGenerators(IEnumerable<IWorldGenerator> generators);
        IWorldGenPipelineBuilder AddInitializers(IEnumerable<IWorldInitializer> initializers);
        WorldGenPipeline Build();
    }
}
