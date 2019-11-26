using System;
using Gradient_color_picker;
using Xunit;

namespace Messerli.GradientColorPickerTest
{
    public sealed class LinearInterpolationTest
    {
        [Theory]
        [MemberData(nameof(GetDataPoints))]
        public void ChecksCalculation(int x1, int x2, int y1, int y2, int value, double result)
        {
            Assert.Equal(LinearInterpolation.GetDataPoint(x1, x2, y1, y2, value), result);
        }

        public static TheoryData<int, int, int, int, int, double> GetDataPoints()
        => new TheoryData<int, int, int, int, int, double>
            {
                { 10, 100, 10, 100, 50, 50 },
                { 10, 50, 20, 100, 100, 200 },
                { 20, 100, 10, 50, 200, 100 },
            };

        [Fact]
        public void ChecksDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() =>
                LinearInterpolation.GetDataPoint(0, 0, 10, 10, 10));
        }
    }
}
