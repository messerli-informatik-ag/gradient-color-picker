﻿using System;
using System.Drawing;
using Messerli.GradientColorPicker;
using Xunit;

namespace Messerli.GradientColorPickerTest
{
    public class IntegrationTest
    {
        private static readonly GradientColorProvider GradientColorProvider = GradientColorBuilder
            .WithColor(new GradientColorByValue(Color.FromArgb(255, 191, 191), 0))
            .Add(new GradientColorByValue(Color.FromArgb(255, 255, 191), 20))
            .Add(new GradientColorByValue(Color.FromArgb(191, 255, 191), 40))
            .Build();

        [Theory]
        [MemberData(nameof(GetEntropy))]
        public void NewAlgorithmProducesSameValesAsOldAlgorithm(double entropy)
        {
            var oldVariantColor = GetOldVariantColor(entropy);
            var newVariantColor = GetNewVariantColor(Convert.ToInt32(entropy));

            AssertAlmostEquals(newVariantColor.R, oldVariantColor.R);
            AssertAlmostEquals(newVariantColor.G, oldVariantColor.G);
            AssertAlmostEquals(newVariantColor.B, oldVariantColor.B);
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

        private static void AssertAlmostEquals(byte lhs, byte rhs)
        {
            const int tolerance = 1;
            Assert.True(Math.Abs(Convert.ToInt32(lhs) - Convert.ToInt32(rhs)) <= tolerance);
        }

        private static Color GetOldVariantColor(double entropy)
        {
            const double baseBrightness = 191.0;
            var red = (int)Math.Min((128.0f * (1 - GetNormalizedMetric(entropy))) + baseBrightness, 255.0);
            var green = (int)Math.Min((128.0f * GetNormalizedMetric(entropy)) + baseBrightness, 255.0);
            const int blue = (int)baseBrightness;

            return Color.FromArgb(red, green, blue);
        }

        private static Color GetNewVariantColor(int entropy)
            => GradientColorProvider.PickColor(entropy);

        private static double GetNormalizedMetric(double entropy)
        {
            const double goodPasswordEntropy = 40.0;
            return Math.Min(entropy / goodPasswordEntropy, 1.0);
        }
    }
}
