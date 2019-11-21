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

        public Color ColorPicker(int value)
        {
            var nextSmallerGradientColorByValue = _gradientColorList.ToList().LastOrDefault(item => item.Value < value);
            var nextHigherGradientColorByValue = _gradientColorList.ToList().FirstOrDefault(item => item.Value > value);

            return GetColor(nextSmallerGradientColorByValue, nextHigherGradientColorByValue, value);
        }

        private static Color GetColor(GradientColorByValue? smallerGradientColorByValue, GradientColorByValue? higherGradientColorByValue, int value)
        {
            if (smallerGradientColorByValue == null && higherGradientColorByValue == null)
            {
                throw new InvalidOperationException();
            }

            if (smallerGradientColorByValue == null)
            {
                return higherGradientColorByValue!.Color;
            }

            if (higherGradientColorByValue == null)
            {
                return smallerGradientColorByValue!.Color;
            }

            var numbersOfValues = Math.Abs(higherGradientColorByValue!.Value - smallerGradientColorByValue.Value);

            var newRed = GetColorValue(smallerGradientColorByValue.Color.R, higherGradientColorByValue.Color.R, value, numbersOfValues);
            var newGreen = GetColorValue(smallerGradientColorByValue.Color.G, higherGradientColorByValue.Color.G, value, numbersOfValues);
            var newBlue = GetColorValue(smallerGradientColorByValue.Color.B, higherGradientColorByValue.Color.B, value, numbersOfValues);

            return Color.FromArgb(newRed, newGreen, newBlue);
        }

        private static int GetColorValue(int startColor, int endColor, int value, int numbersOfValues)
        {
            return (value * startColor) + ((numbersOfValues - value) * endColor);
        }
    }
}
