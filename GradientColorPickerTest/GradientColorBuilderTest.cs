using System;
using System.Drawing;
using Messerli.GradientColorPicker;
using Xunit;
using static Messerli.GradientColorPicker.GradientColorBuilder;

namespace Messerli.GradientColorPickerTest
{
    public partial class GradientColorBuilderTest
    {
        [Fact]
        public void CreatesSingleGradientColor()
        {
            var gradientColorProvider = WithColor(new GradientColorByValue(Color.Red, -10))
                .Build();

            Assert.Equal(gradientColorProvider.PickColor(-10).ToArgb(), Color.Red.ToArgb());
        }

        [Fact]
        public void CreatesMultipleGradientColors()
        {
            var gradientColorProvider = WithColor(new GradientColorByValue(Color.Red, -10))
                .Add(new GradientColorByValue(Color.Green, 0))
                .Add(new GradientColorByValue(Color.Blue, 10))
                .Build();

            Assert.Equal(gradientColorProvider.PickColor(0).ToArgb(), Color.Green.ToArgb());
        }

        [Fact]
        public void ErrorsOnInitializeAndAddSameValue()
        {
            const int sameValue = 0;
            Assert.Throws<InvalidOperationException>(() =>
                WithColor(new GradientColorByValue(Color.Red, sameValue))
                    .Add(new GradientColorByValue(Color.Green, sameValue))
                    .Add(new GradientColorByValue(Color.Blue, 10))
                    .Build());
        }

        [Fact]
        public void ErrorsOnAddSameValue()
        {
            const int sameValue = 0;
            Assert.Throws<InvalidOperationException>(() =>
                WithColor(new GradientColorByValue(Color.Red, -10))
                    .Add(new GradientColorByValue(Color.Green, sameValue))
                    .Add(new GradientColorByValue(Color.Blue, sameValue))
                    .Build());
        }
    }
}
