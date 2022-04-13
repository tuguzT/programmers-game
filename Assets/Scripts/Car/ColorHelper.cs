using System;
using Random = UnityEngine.Random;

namespace Car
{
    public static class ColorHelper
    {
        public static Color GetRandom()
        {
            var values = Enum.GetValues(typeof(Color));
            return (Color)values.GetValue(Random.Range(0, values.Length));
        }
    }
}
