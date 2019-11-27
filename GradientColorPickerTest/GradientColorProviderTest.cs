using System.Drawing;
using Messerli.GradientColorPicker;
using Xunit;

namespace Messerli.GradientColorPickerTest
{
    public partial class GradientColorBuilderTest
    {
        private const int RedColorValue = -10;
        private const int GreenColorValue = 0;
        private const int BlueColorValue = 10;

        private readonly GradientColorProvider _gradientColorProvider = GradientColorBuilder
            .WithColor(new GradientColorByValue(Color.Red, RedColorValue))
            .Add(new GradientColorByValue(Color.Green, GreenColorValue))
            .Add(new GradientColorByValue(Color.Blue, BlueColorValue))
            .Build();

        [Fact]
        public void ChecksGradientColorPicker()
        {
            Assert.Equal(_gradientColorProvider.PickColor(RedColorValue).ToArgb(), Color.Red.ToArgb());
            Assert.Equal(_gradientColorProvider.PickColor(GreenColorValue).ToArgb(), Color.Green.ToArgb());
            Assert.Equal(_gradientColorProvider.PickColor(BlueColorValue).ToArgb(), Color.Blue.ToArgb());
        }

        [Fact]
        public void UsesMinimumPositionRangeOfColor()
        {
            Assert.Equal(_gradientColorProvider.PickColor(-20), Color.Red);
        }

        [Fact]
        public void UsesMaximumPositionRangeOfColor()
        {
            Assert.Equal(_gradientColorProvider.PickColor(20), Color.Blue);
        }

        [Theory]
        [MemberData(nameof(GetGradientColorData))]
        public void CalculatesLinearInterpolationForGradientColor(int value, Color selectedColor)
        {
            Assert.Equal(_gradientColorProvider.PickColor(value).ToArgb(), selectedColor.ToArgb());
        }

        public static TheoryData<int, Color> GetGradientColorData()
            => new TheoryData<int, Color>
            {
                { RedColorValue, Color.Red },
                { GreenColorValue, Color.Green },
                { BlueColorValue, Color.Blue },
                { -2, Color.FromArgb(51, 102, 0) },
                { 2, Color.FromArgb(0, 103, 51) },
                { -5, Color.FromArgb(128, 64, 0) },
                { 5, Color.FromArgb(0, 64, 127) },
            };
    }
}
