using System;
using TerrariaClone.Features.WorldStreaming.Chunks.Persistence;

namespace TerrariaClone.Features.WorldStreaming.Pipeline
{
    public interface IWorldStreamingPipelineBuilder
    {
        IWorldStreamingPipelineBuilder Configure(Action<WorldStreamingOptions> options);
        IWorldStreamingPipelineBuilder WithChunkReader(IChunkReader chunkReader);
        IWorldStreamingPipelineBuilder WithChunkWriter(IChunkWriter chunkWriter);
        IWorldStreamingPipelineBuilder WithChunkPathProvider(IChunkPathProvider pathProvider);
        WorldStreamingPipeline Build();
    }
}
