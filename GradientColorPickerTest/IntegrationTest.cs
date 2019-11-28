using System;
using System.Drawing;
using Messerli.GradientColorPicker;
using Xunit;

namespace Messerli.GradientColorPickerTest
{
    public class IntegrationTest
    {
        private const double GoodPasswordEntropy = 40.0;
        private const double BaseBrightness = 191.0;
        private static readonly GradientColorProvider GradientColorProvider = GradientColorBuilder
            .WithColor(new GradientColorByValue(Color.FromArgb(255, 191, 191), 0))
            .Add(new GradientColorByValue(Color.FromArgb(255, 255, 191), 20))
            .Add(new GradientColorByValue(Color.FromArgb(191, 255, 191), 40))
            .Build();

        [Theory]
        [MemberData(nameof(GetEntropy))]
        public void PasswordStrengthColor(double entropy)
        {
            var oldVariantColor = GetOldVariantColor(entropy);
            var newVariantColor = GetNewVariantColor(Convert.ToInt32(entropy));

            Assert.True(newVariantColor.R - oldVariantColor.R <= 1);
            Assert.True(newVariantColor.G - oldVariantColor.G <= 1);
            Assert.True(newVariantColor.B - oldVariantColor.B <= 1);
        }

        public static TheoryData<double> GetEntropy()
            => new TheoryData<double>
            {
                { 0 },
                { 10 },
                { 16 },
                { 20 },
                { 27 },
                { 40 },
                { 50 },
                { 100 },
            };

        private static Color GetOldVariantColor(double entropy)
        {
            var red = (int)Math.Min((128.0f * (1 - GetNormalizedMetric(entropy))) + BaseBrightness, 255.0);
            var green = (int)Math.Min((128.0f * GetNormalizedMetric(entropy)) + BaseBrightness, 255.0);
            const int blue = (int)BaseBrightness;

            return Color.FromArgb(red, green, blue);
        }

        private static Color GetNewVariantColor(int entropy)
            => GradientColorProvider.PickColor(entropy);

        private static double GetNormalizedMetric(double entropy)
        {
            return Math.Min(entropy / GoodPasswordEntropy, 1.0);
        }
    }
}
