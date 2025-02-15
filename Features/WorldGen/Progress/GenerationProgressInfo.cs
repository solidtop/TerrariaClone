namespace TerrariaClone.Features.WorldGen.Progress
{
    public class GenerationProgressInfo(GenerationPass pass, float localProgress, float totalProgress)
    {
        public GenerationProgressInfo(GenerationPass pass, float localProgress) : this(pass, localProgress, 0) { }

        public GenerationPass Pass { get; } = pass;
        public float LocalProgress { get; } = localProgress;
        public float TotalProgress { get; } = totalProgress;
        public string DisplayName => GenerationPassNames.GetName(Pass);
    }
}
