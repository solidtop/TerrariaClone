using System;
using TerrariaClone.Common.Serialization;
using TerrariaClone.Features.WorldStreaming.Chunks;
using TerrariaClone.Features.WorldStreaming.Chunks.Persistence;

namespace TerrariaClone.Features.WorldStreaming.Pipeline
{
    public class WorldStreamingPipelineBuilder : IWorldStreamingPipelineBuilder
    {
        private static readonly ISerializer DefaultSerializer = new MessagePackAdapter();
        private static readonly IChunkReader DefaultChunkReader = new ChunkReader(DefaultSerializer);
        private static readonly IChunkWriter DefaultChunkWriter = new ChunkWriter(DefaultSerializer);
        private static readonly IChunkPathProvider DefaultChunkPathProvider = new ChunkPathProvider("user://");

        private IChunkReader _chunkReader = DefaultChunkReader;
        private IChunkWriter _chunkWriter = DefaultChunkWriter;
        private IChunkPathProvider _chunkPathProvider = DefaultChunkPathProvider;

        private readonly WorldStreamingOptions _options = new();

        public IWorldStreamingPipelineBuilder Configure(Action<WorldStreamingOptions> options)
        {
            options?.Invoke(_options);
            return this;
        }

        public IWorldStreamingPipelineBuilder WithChunkReader(IChunkReader chunkReader)
        {
            _chunkReader = chunkReader;
            return this;
        }

        public IWorldStreamingPipelineBuilder WithChunkWriter(IChunkWriter chunkWriter)
        {
            _chunkWriter = chunkWriter;
            return this;
        }

        public IWorldStreamingPipelineBuilder WithChunkPathProvider(IChunkPathProvider pathProvider)
        {
            _chunkPathProvider = pathProvider;
            return this;
        }

        public WorldStreamingPipeline Build()
        {
            var chunkRepository = new ChunkRepository(_chunkReader, _chunkWriter, _chunkPathProvider);
            var chunkStreamer = new ChunkStreamer(chunkRepository, _options.ChunkSize, _options.StreamDistance);

            var worldStreamer = new WorldStreamer(chunkStreamer);

            return new WorldStreamingPipeline(worldStreamer);
        }
    }
}
