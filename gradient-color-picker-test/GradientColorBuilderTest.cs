using System;
using System.Drawing;
using Gradient_color_picker;
using Xunit;
using static Gradient_color_picker.GradientColorBuilder;

namespace Gradient_color_picker_test
{
    public partial class GradientColorBuilderTest
    {
        [Fact]
        public void CreateGradientColorBuilder()
        {
            var gradientColorProvider = WithColor(new GradientColorByValue(Color.Red, -10))
                .Add(new GradientColorByValue(Color.Green, 0))
                .Add(new GradientColorByValue(Color.Blue, 10))
                .Build();

            Assert.Equal(gradientColorProvider.ColorPicker(0), Color.Green);
        }

        [Fact]
        public void ErrorsOnSameValueAdded()
        {
            const int sameValue = 0;
            Assert.Throws<InvalidOperationException>(() =>
                WithColor(new GradientColorByValue(Color.Green, sameValue))
                    .Add(new GradientColorByValue(Color.Green, sameValue))
                    .Build());
        }
    }
}
