using System;
using System.Collections.Immutable;
using System.Linq;

namespace Messerli.GradientColorPicker
{
    public class GradientColorBuilder
    {
        private readonly IImmutableList<GradientColorByValue> _gradientColorList = ImmutableList<GradientColorByValue>.Empty;

        private GradientColorBuilder()
        {
        }

        private GradientColorBuilder(IImmutableList<GradientColorByValue> gradientColorList)
        {
            _gradientColorList = gradientColorList;
        }

        public static GradientColorBuilder WithColor(GradientColorByValue colorByValue)
            => new GradientColorBuilder().Add(colorByValue);

        public GradientColorBuilder Add(GradientColorByValue gradientColor)
        {
            if (_gradientColorList.Any(item => item.Value == gradientColor.Value))
            {
                throw new InvalidOperationException($"Gradient color value ({gradientColor.Value}) is already defined");
            }

            return ShallowClone(_gradientColorList.Add(gradientColor));
        }

        public GradientColorProvider Build()
            => new GradientColorProvider(_gradientColorList);

        private static GradientColorBuilder ShallowClone(IImmutableList<GradientColorByValue> gradientColors)
            => new GradientColorBuilder(gradientColors);
    }
}
