namespace Lang.Extensions
{
    public static class NumericExtensions
    {
        ///<comment>
        /// Remaps value in the range from1 -> to1 to the range from2 -> to2
        ///</comment>
        public static float Remap (this float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        ///<comment>
        /// Clamps value in the range from1 -> to1 to the range from2 -> to2
        ///</comment>
        public static float Clamp (this float value, float min, float max) {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        ///<comment>
        /// Clamps value in the range from1 -> to1 to the range from2 -> to2
        ///</comment>
        public static int Clamp (this int value, int min, int max) {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }
    }
}