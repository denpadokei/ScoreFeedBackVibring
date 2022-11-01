namespace ScoreFeedBackVibring.Configuration
{
    internal class VibroParam
    {
        public virtual float MaxDistanceToCenter { get; set; } = 0.01f;
        public virtual float Duration { get; set; } = 0.05f;
        public virtual float Strength { get; set; } = 1f;
        public virtual float Frequency { get; set; } = 0.5f;
    }
}
