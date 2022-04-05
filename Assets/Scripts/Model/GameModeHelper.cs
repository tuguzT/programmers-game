using System;

namespace Model
{
    public static class GameModeHelper
    {
        public static uint FieldWidth(this GameMode gameMode) => gameMode switch
        {
            GameMode.Easy => 6,
            GameMode.Hard => 9,
            _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null),
        };
    }
}
