using Godot;
using TerrariaClone.Common.Serialization;
using TerrariaClone.Features.Chunks.Persistence;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Debug;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;
using TerrariaClone.Features.WorldGen.Pipeline;

namespace TerrariaClone;

public partial class Main : Node
{
    public override async void _Ready()
    {
        var builder = new WorldGenPipelineBuilder();

        builder.Configure(options =>
        {
            options.Seed = 12345;
            options.ConfigPath = "./Data/Worlds/TestWorld/WorldGen";
        });

        builder.AddGenerators([
            new TerrainGenerator(),
            new StoneGenerator(),
            new DirtGenerator(),
            new SmallCaveGenerator(),
            new LargeCaveGenerator(),
            ]);

        builder.AddInitializers([
             new PartitionInitializer(new WorldPartitioner()),
                new SaveInitializer(new ChunkWriter(new MessagePackAdapter())),
                new SpawnPointInitializer(),
            ]);

        builder.AddDebugger(new WorldGenProgressReporter());
        builder.AddDebugger(new WorldGenDebugRenderer());

        var pipeline = builder.Build();

        await pipeline.RunAsync(this);
    }
}
