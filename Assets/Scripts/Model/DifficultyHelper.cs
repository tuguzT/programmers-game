using System;

namespace Model
{
    public static class DifficultyHelper
    {
        public static uint FieldWidth(this Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => 6,
                Difficulty.Hard => 9,
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };
        }
    }
}
