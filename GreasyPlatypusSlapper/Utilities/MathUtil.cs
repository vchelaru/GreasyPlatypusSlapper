using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreasyPlatypusSlapper.Utilities
{
    public static class MathUtil
    {
        public static float FloatInRange(this Random rand, float min, float max)
        {
            // early out for same value
            if (max == min)
            {
                return max;
            }

            var range = max - min;
            var randInRange = (float)(rand.NextDouble() * range);
            return min + randInRange;
        }
    }
}
