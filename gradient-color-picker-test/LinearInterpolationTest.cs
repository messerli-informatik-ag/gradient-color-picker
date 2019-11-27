using System;
using System.Drawing;
using Messerli.GradientColorPicker;
using Xunit;

namespace Messerli.GradientColorPickerTest
{
    public sealed class LinearInterpolationTest
    {
        [Theory]
        [MemberData(nameof(GetDataPoints))]
        public void ChecksCalculation(Point p1, Point p2, int value, double result)
        {
            Assert.Equal(LinearInterpolation.CalculateInterpolationValue(p1, p2, value), result);
        }

        public static TheoryData<Point, Point, int, int> GetDataPoints()
        => new TheoryData<Point, Point, int, int>
            {
                { new Point(10, 10), new Point(100, 100), 50, 50 },
                { new Point(10, 20),  new Point(50, 100), 100, 200 },
                { new Point(20, 10), new Point(100, 50), 200, 100 },
            };

        [Fact]
        public void ChecksDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() =>
                LinearInterpolation.CalculateInterpolationValue(new Point(0, 10), new Point(0, 10), 10));
        }
    }
}
