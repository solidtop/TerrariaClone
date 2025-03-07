using Godot;
using TerrariaClone.Common.Definitions;
using TerrariaClone.Common.Serialization;
using TerrariaClone.Features.WorldGen.Debug;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;
using TerrariaClone.Features.WorldGen.Pipeline;
using TerrariaClone.Features.WorldStreaming.Chunks.Debug;
using TerrariaClone.Features.WorldStreaming.Chunks.Persistence;
using TerrariaClone.Features.WorldStreaming.Pipeline;

namespace TerrariaClone;

public partial class Main : Node
{
    private bool _showWorldGenDebug = true;
    private bool _showChunkDebug = false;

    public class SharedOptions()
    {
        public WorldDefinitions WorldDefinitions { get; init; }
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
            WorldDefinitions = WorldDefinitions.Load("./Data/Worlds/TestWorld"),
            ChunkSize = new(16, 16),
            ChunkPathProvider = new ChunkPathProvider("user://"),
            Serializer = serializer,
            ChunkWriter = new ChunkWriter(serializer),
        };

        // Build the pipelines
        var worldGenPipeline = BuildWorldGenPipeline(shared);
        var streamPipeline = BuildStreamingPipeline(shared);

        if (_showWorldGenDebug)
            EnableWorldGenDebug(worldGenPipeline);

        // Run the pipelines
        await worldGenPipeline.RunAsync();
        //streamPipeline.Run(this);

        //if (_showChunkDebug)
        //    EnableStreamingDebug(streamPipeline);

        //var blockRenderer = new BlockRenderer(streamPipeline.ChunkStreamer);
        //AddChild(blockRenderer);
    }

    private static WorldGenPipeline BuildWorldGenPipeline(SharedOptions shared)
    {
        return new WorldGenPipelineBuilder(shared.WorldDefinitions).Configure(options =>
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
        return new WorldStreamingPipelineBuilder(shared.WorldDefinitions).Configure(options =>
        {
            options.ChunkSize = shared.ChunkSize;
            options.StreamDistance = new(3, 2);
        })
        .WithChunkReader(new ChunkReader(shared.Serializer))
        .WithChunkWriter(shared.ChunkWriter)
        .WithChunkPathProvider(shared.ChunkPathProvider)
        .Build();
    }

    private void EnableWorldGenDebug(WorldGenPipeline pipeline)
    {
        pipeline.ProgressMonitor.ProgressChanged += (taskName, progress) =>
        {
            GD.Print($"{taskName}: {Mathf.FloorToInt(progress * 100)}%");
        };

        var debugRenderer = new WorldGenDebugRenderer(pipeline.WorldGenerator);
        AddChild(debugRenderer);
    }

    private void EnableStreamingDebug(WorldStreamingPipeline pipeline)
    {
        var debugRenderer = new ChunkDebugRenderer(pipeline.ChunkStreamer);
        AddChild(debugRenderer);
    }
}
