using System;
using Random = UnityEngine.Random;

namespace Model
{
    public static class TeamColorHelper
    {
        public static TeamColor GetRandom()
        {
            var values = Enum.GetValues(typeof(TeamColor));
            return (TeamColor)values.GetValue(Random.Range(0, values.Length));
        }
    }
}
