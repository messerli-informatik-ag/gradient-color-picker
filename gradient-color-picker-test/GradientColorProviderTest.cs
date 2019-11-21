using System.Drawing;
using Gradient_color_picker;
using Xunit;

namespace Gradient_color_picker_test
{
    public partial class GradientColorBuilderTest
    {
        private const int RedColorValue = -10;
        private const int GreenColorValue = 0;
        private const int BlueColorValue = 10;

        private readonly GradientColorProvider _gradientColorProvider = GradientColorBuilder.WithColor(new GradientColorByValue(Color.Red, RedColorValue))
            .Add(new GradientColorByValue(Color.Green, GreenColorValue))
            .Add(new GradientColorByValue(Color.Blue, BlueColorValue))
            .Build();

        [Fact]
        public void ChecksGradientColorPicker()
        {
            Assert.Equal(_gradientColorProvider.ColorPicker(RedColorValue), Color.Red);
            Assert.Equal(_gradientColorProvider.ColorPicker(GreenColorValue), Color.Green);
            Assert.Equal(_gradientColorProvider.ColorPicker(BlueColorValue), Color.Blue);
        }

        [Fact]
        public void UsesMinimumPositionRangeOfColor()
        {
            Assert.Equal(_gradientColorProvider.ColorPicker(-20), Color.Red);
        }

        [Fact]
        public void UsesMaximumPositionRangeOfColor()
        {
            Assert.Equal(_gradientColorProvider.ColorPicker(20), Color.Blue);
        }

        [Fact]
        public void CalculatesLinearInterpolationForGradientColor()
        {
            var selectedColor = _gradientColorProvider.ColorPicker(5);
            Assert.True(selectedColor.Equals(Color.FromArgb(0, 64, 127)));
        }
    }
}
