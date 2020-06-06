using System;

namespace Trader.Helpers.Common
{
    public static class RandomExtensions
    {
        public static double NextDoubleInRange(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
