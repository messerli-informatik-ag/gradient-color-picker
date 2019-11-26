using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace Gradient_color_picker
{
    public class GradientColorProvider : IGradientColorProvider
    {
        private readonly IReadOnlyCollection<GradientColorByValue> _gradientColorList;

        public GradientColorProvider(IImmutableList<GradientColorByValue> gradientColorList)
        {
            _gradientColorList = gradientColorList;
        }

        public Color PickColor(int value)
        {
            if (_gradientColorList.Any(item => item.Value == value))
            {
                return _gradientColorList.First(item => item.Value == value).Color;
            }

            var nextSmallerGradientColorByValue = _gradientColorList.ToList().LastOrDefault(item => item.Value < value);
            var nextHigherGradientColorByValue = _gradientColorList.ToList().FirstOrDefault(item => item.Value > value);

            return GetColor(nextSmallerGradientColorByValue, nextHigherGradientColorByValue, value);
        }

        private static Color GetColor(GradientColorByValue? smallerGradientColorByValue, GradientColorByValue? higherGradientColorByValue, int value)
        {
            if (smallerGradientColorByValue is null && higherGradientColorByValue is null)
            {
                throw new InvalidOperationException();
            }

            if (smallerGradientColorByValue is null)
            {
                return higherGradientColorByValue!.Color;
            }

            if (higherGradientColorByValue is null)
            {
                return smallerGradientColorByValue!.Color;
            }

            var newRed = LinearInterpolation.GetDataPoint(smallerGradientColorByValue.Value, higherGradientColorByValue.Value, smallerGradientColorByValue.Color.R, higherGradientColorByValue.Color.R, value);
            var newGreen = LinearInterpolation.GetDataPoint(smallerGradientColorByValue.Value, higherGradientColorByValue.Value, smallerGradientColorByValue.Color.G, higherGradientColorByValue.Color.G, value);
            var newBlue = LinearInterpolation.GetDataPoint(smallerGradientColorByValue.Value, higherGradientColorByValue.Value, smallerGradientColorByValue.Color.B, higherGradientColorByValue.Color.B, value);

            return Color.FromArgb(Convert.ToInt32(newRed), Convert.ToInt32(newGreen), Convert.ToInt32(newBlue));
        }
    }
}
