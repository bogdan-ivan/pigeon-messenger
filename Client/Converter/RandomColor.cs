using System;

namespace Client
{
    public class RandomColor
    {
        public static string generateColor()
        {
            var random = new Random();
            var color = String.Format("{0:X6}", random.Next(0x1000000));

            return color.ToLower();
        }
    }
}
