using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace Messerli.GradientColorPicker
{
    public class GradientColorProvider : IGradientColorProvider
    {
        private readonly IReadOnlyCollection<GradientColorByValue> _gradientColorList;

        public GradientColorProvider(IImmutableList<GradientColorByValue> gradientColorList)
        {
            _gradientColorList = gradientColorList.OrderBy(item => item.Value).ToImmutableList();
        }

        public Color PickColor(int value)
        {
            if (!_gradientColorList.Any())
            {
                throw new InvalidOperationException("No gradient color defined");
            }

            if (value <= _gradientColorList.First().Value)
            {
                return _gradientColorList.First().Color;
            }

            if (value >= _gradientColorList.Last().Value)
            {
                return _gradientColorList.Last().Color;
            }

            var resultPair = _gradientColorList
                .Pairwise()
                .First(pair => value >= pair.First.Value && value <= pair.Second.Value);

            return GetColor(resultPair.First, resultPair.Second, value);
        }

        private static Color GetColor(GradientColorByValue smallerGradientColorByValue, GradientColorByValue higherGradientColorByValue, int value)
        {
            var newRed = LinearInterpolation.GetDataPoint(smallerGradientColorByValue.Value, higherGradientColorByValue.Value, smallerGradientColorByValue.Color.R, higherGradientColorByValue.Color.R, value);
            var newGreen = LinearInterpolation.GetDataPoint(smallerGradientColorByValue.Value, higherGradientColorByValue.Value, smallerGradientColorByValue.Color.G, higherGradientColorByValue.Color.G, value);
            var newBlue = LinearInterpolation.GetDataPoint(smallerGradientColorByValue.Value, higherGradientColorByValue.Value, smallerGradientColorByValue.Color.B, higherGradientColorByValue.Color.B, value);

            return Color.FromArgb(Convert.ToInt32(newRed), Convert.ToInt32(newGreen), Convert.ToInt32(newBlue));
        }
    }

    internal static class EnumerableExtensions
    {
        public static IEnumerable<(T First, T Second)> Pairwise<T>(this IEnumerable<T> source)
        {
            using (var it = source.GetEnumerator())
            {
                if (it.MoveNext())
                {
                    var previous = it.Current;
                    while (it.MoveNext())
                    {
                        yield return (previous, previous = it.Current);
                    }
                }
            }
        }
    }
}
