namespace Gradient_color_picker
{
    public class LinearInterpolation
    {
        public static int GetDataPoint(int startRange, int endRange, int value, int numbersOfSteps)
        {
            return ((value * startRange) + ((numbersOfSteps - value) * endRange)) / numbersOfSteps;
        }
    }
}
