using System;

namespace ScoreFeedBackVibring.Configuration
{
    internal class VibroParam : IEquatable<VibroParam>
    {
        public virtual float MaxDistanceToCenter { get; set; } = 0.01f;
        public virtual float Duration { get; set; } = 0.05f;
        public virtual float Strength { get; set; } = 1f;
        public virtual float Frequency { get; set; } = 0.5f;

        public override bool Equals(object obj)
        {
            return this.Equals(obj as VibroParam);
        }

        public bool Equals(VibroParam other)
        {
            return !(other is null) &&
                   this.MaxDistanceToCenter == other.MaxDistanceToCenter &&
                   this.Duration == other.Duration &&
                   this.Strength == other.Strength &&
                   this.Frequency == other.Frequency;
        }

        public override int GetHashCode()
        {
            var hashCode = -124972068;
            hashCode = (hashCode * -1521134295) + this.MaxDistanceToCenter.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Duration.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Strength.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Frequency.GetHashCode();
            return hashCode;
        }
    }
}
