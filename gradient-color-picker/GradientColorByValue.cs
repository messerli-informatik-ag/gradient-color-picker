using System.Drawing;

#pragma warning disable CS0660, CS0661

namespace Messerli.GradientColorPicker
{
    [Equals]
    public class GradientColorByValue
    {
        public GradientColorByValue(Color color, int value)
        {
            Color = color;
            Value = value;
        }

        public Color Color { get; }

        public int Value { get; }

        public static bool operator ==(GradientColorByValue left, GradientColorByValue right) => Operator.Weave(left, right);

        public static bool operator !=(GradientColorByValue left, GradientColorByValue right) => Operator.Weave(left, right);
    }
}
