using System.Drawing;

namespace Messerli.GradientColorPicker
{
    public static class LinearInterpolation
    {
        public static int CalculateInterpolationValue(Point p1, Point p2, int x)
        {
            return ((x - p1.X) * (p2.Y - p1.Y) / (p2.X - p1.X)) + p1.Y;
        }
    }
}
