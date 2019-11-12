using System.Drawing;

namespace gradient_color_picker
{
    class GradientColorController : IGradientColorController
    {
        public Color ColorPicker()
        {
            int red = (int)System.Math.Min(128.0f * (1 - GetNormalizedMetric()) + baseBrightness, 255.0);
            int green = (int)System.Math.Min(128.0f * GetNormalizedMetric() + baseBrightness, 255.0);
            int blue = (int)baseBrightness;

            return System.Drawing.Color.FromArgb(red, green, blue);
        }
    }

    private double GetNormalizedMetric()
    {
        return System.Math.Min(Entropy / GoodPasswordEntropy, 1.0);
    }
}
