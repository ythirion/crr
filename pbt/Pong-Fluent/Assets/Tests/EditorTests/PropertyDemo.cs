using System;
using FsCheck;
using NUnit.Framework;

namespace EditorTests
{
    public class PropertyDemo
    {
        private static readonly Arbitrary<double> ValidDoubles = Arb.Default
                .Float()
                .Filter(d => !double.IsInfinity(d) && !double.IsNaN(d));
        
        private static readonly Arbitrary<(Angle x, Angle y)> AngleTuples = (from x in ValidDoubles.Generator
            from y in ValidDoubles.Generator
            select (x.ToAngle(), y.ToAngle())).ToArbitrary();
        
        [Test]
        public void Normalize_Value() 
            => Prop.ForAll(ValidDoubles, x =>
                {
                    TestContext.Out.WriteLine(x);
                    return IsNormalized(x.ToAngle());
                })
                .QuickCheckThrowOnFailure();
        
        [Test]
        public void Addition_Normalize() 
            => Prop.ForAll(AngleTuples, p => IsNormalized(p.x + p.y))
                .QuickCheckThrowOnFailure();
        
        [Test]
        public void Addition_Is_Commutative() 
            => Prop.ForAll(AngleTuples, p => p.x + p.y == p.y + p.x)
                .QuickCheckThrowOnFailure();
        
        [Test]
        public void Subtract_Normalize() 
            => Prop.ForAll(AngleTuples, p => IsNormalized(p.x - p.y))
                .QuickCheckThrowOnFailure();

        private static bool IsNormalized(Angle angle) => angle.Value is >= 0 and < 360;
    }
}