using Godot;
using TerrariaClone.Common.Serialization;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;
using TerrariaClone.Features.WorldGen.Pipeline;
using TerrariaClone.Features.WorldStreaming.Chunks.Debug;
using TerrariaClone.Features.WorldStreaming.Chunks.Persistence;
using TerrariaClone.Features.WorldStreaming.Pipeline;

namespace TerrariaClone;

public partial class Main : Node
{
    public class SharedOptions()
    {
        public Vector2I ChunkSize { get; init; }
        public IChunkPathProvider ChunkPathProvider { get; init; }
        public ISerializer Serializer { get; init; }
        public IChunkWriter ChunkWriter { get; init; }
    }

    public override async void _Ready()
    {
        var serializer = new MessagePackAdapter();

        var shared = new SharedOptions()
        {
            ChunkSize = new(16, 16),
            ChunkPathProvider = new ChunkPathProvider("user://"),
            Serializer = serializer,
            ChunkWriter = new ChunkWriter(serializer),
        };

        // Build the pipelines
        var worldGenPipeline = BuildWorldGenPipeline(shared);
        var streamPipeline = BuildStreamingPipeline(shared);

        worldGenPipeline.ProgressMonitor.ProgressChanged += (taskName, progress) =>
        {
            GD.Print($"{taskName}: {Mathf.FloorToInt(progress * 100)}%");
        };

        // Run the pipelines
        await worldGenPipeline.RunAsync();
        streamPipeline.Run(this);

        var chunkDebug = new ChunkDebugRenderer(streamPipeline.ChunkStreamer);
        AddChild(chunkDebug);

        var tileRenderer = new TileRenderer(streamPipeline.ChunkStreamer);
        AddChild(tileRenderer);
    }

    private static WorldGenPipeline BuildWorldGenPipeline(SharedOptions shared)
    {
        return new WorldGenPipelineBuilder().Configure(options =>
        {
            options.Seed = 12345;
            options.ConfigPath = "./Data/Worlds/TestWorld/WorldGen";
            options.RegionSize = new(64, 64);
            options.ChunkSize = shared.ChunkSize;
        })
        .AddGenerators([
            new TerrainGenerator("Generating terrain"),
            new StoneGenerator("Placing stone in the dirt"),
            new DirtGenerator("Placing dirt in the stone"),
            new SmallCaveGenerator("Generating small caves"),
            new LargeCaveGenerator("Generating large caves"),
        ])
        .AddInitializers([
            new WorldPartitioner("Partitioning world"),
            new WorldSaver("Saving world", shared.ChunkWriter, shared.ChunkPathProvider),
            new SpawnPointPicker("Picking player spawn point"),
        ])
        .Build();
    }

    private static WorldStreamingPipeline BuildStreamingPipeline(SharedOptions shared)
    {
        return new WorldStreamingPipelineBuilder().Configure(options =>
        {
            options.ChunkSize = shared.ChunkSize;
            options.StreamDistance = new(5, 3);
        })
        .WithChunkReader(new ChunkReader(shared.Serializer))
        .WithChunkWriter(shared.ChunkWriter)
        .WithChunkPathProvider(shared.ChunkPathProvider)
        .Build();
    }
}
