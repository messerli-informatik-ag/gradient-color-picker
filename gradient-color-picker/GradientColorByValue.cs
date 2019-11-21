using System.Drawing;

namespace Gradient_color_picker
{
    internal class GradientColorByValue
    {
        public GradientColorByValue(Color color, int value)
        {
            Color = color;
            Value = value;
        }

        public Color Color { get; }

        public int Value { get; }
    }
}
