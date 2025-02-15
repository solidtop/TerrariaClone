namespace TerrariaClone.Features.WorldGen.Progress
{
    public class WorldGenProgressInfo(WorldGenPass pass, float localProgress, float totalProgress)
    {
        public WorldGenProgressInfo(WorldGenPass pass, float localProgress) : this(pass, localProgress, 0) { }

        public WorldGenPass Pass { get; } = pass;
        public float LocalProgress { get; } = localProgress;
        public float TotalProgress { get; } = totalProgress;
        public string DisplayName => WorldGenPassNames.GetName(Pass);
    }
}
