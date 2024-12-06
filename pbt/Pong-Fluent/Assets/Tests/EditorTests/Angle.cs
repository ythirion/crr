using System;

namespace EditorTests
{
    public readonly struct Angle : IEquatable<Angle>
    {
        private const double Tolerance = 0.0000001;
        public double Value { get; }
        public Angle(double degrees) => Value = Normalize(degrees);
        private static double Normalize(double value) => (value % 360 + 360) % 360;
        public static Angle operator +(Angle a, Angle b) => new(a.Value + b.Value);
        public static Angle operator -(Angle a, Angle b) => new(a.Value - b.Value);
        public static bool operator ==(Angle a, Angle b) => Math.Abs(a.Value - b.Value) < Tolerance;
        public static bool operator !=(Angle a, Angle b) => !(a == b);
        public override string ToString() => $"{Value}°";
        public bool Equals(Angle other) => Value.Equals(other.Value);
        public override bool Equals(object obj) => obj is Angle other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
    }
    
    public static class DoubleExtensions
    {
        public static Angle ToAngle(this double degrees) => new(degrees);
    }
}