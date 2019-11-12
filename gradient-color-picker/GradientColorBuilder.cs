using System.Collections.Generic;
using System.Collections.Immutable;

namespace gradient_color_picker
{
    class GradientColorBuilder
    {
        private readonly IImmutableList<GradientColor> _gradientColorList;

        public GradientColorBuilder()
        {
            _gradientColorList = new List<GradientColor>();
        }

        public GradientColorBuilder(List<GradientColor> gradientColorList)
        {
            _gradientColorList = gradientColorList;
        }

        public GradientColorBuilder AddGradientColor(GradientColor gradientColor)
        {
            return DeepClone(_gradientColorList.Add(gradientColor));
        }

        public GradientColorController Build()
        {
            return new GradientColorController();
        }

        private GradientColorBuilder DeepClone(List<GradientColor> gradientColors)
        {
            return new GradientColorBuilder(gradientColors);
        }
    }
}
