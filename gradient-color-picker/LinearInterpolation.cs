namespace Gradient_color_picker
{
    public static class LinearInterpolation
    {
        public static double GetDataPoint(int x1, int x2, int y1, int y2, int x)
        {
            return ((x - x1) * (y2 - y1) / (x2 - x1)) + y1;
        }
    }
}
