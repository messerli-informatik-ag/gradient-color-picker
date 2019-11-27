using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Messerli.GradientColorPicker
{
    public class GradientColorProvider : IGradientColorProvider
    {
        private readonly IReadOnlyCollection<GradientColorByValue> _gradientColorList;

        public GradientColorProvider(IImmutableList<GradientColorByValue> gradientColorList)
        {
            if (!gradientColorList.Any())
            {
                throw new InvalidOperationException("No gradient color defined");
            }

            _gradientColorList = gradientColorList.OrderBy(item => item.Value).ToImmutableList();
        }

        public Color PickColor(int value)
        {
            if (value <= _gradientColorList.First().Value)
            {
                return _gradientColorList.First().Color;
            }

            if (value >= _gradientColorList.Last().Value)
            {
                return _gradientColorList.Last().Color;
            }

            return GetColor(FindPair(value), value);
        }

        public (GradientColorByValue First, GradientColorByValue Second) FindPair(int value)
            => _gradientColorList
                .Pairwise()
                .First(pair => value >= pair.First.Value && value <= pair.Second.Value);

        private static Color GetColor((GradientColorByValue First, GradientColorByValue Second) pair, int value)
        {
            var red = LinearInterpolation.CalculateInterpolationValue(
                CreatePoint(pair.First.Value, pair.First.Color.R),
                CreatePoint(pair.Second.Value, pair.Second.Color.R),
                value);

            var green = LinearInterpolation.CalculateInterpolationValue(
                CreatePoint(pair.First.Value, pair.First.Color.G),
                CreatePoint(pair.Second.Value, pair.Second.Color.G),
                value);

            var blue = LinearInterpolation.CalculateInterpolationValue(
                CreatePoint(pair.First.Value, pair.First.Color.B),
                CreatePoint(pair.Second.Value, pair.Second.Color.B),
                value);

            return Color.FromArgb(red, green, blue);
        }

        private static Point CreatePoint(int x, int y)
            => new Point(x, y);
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
