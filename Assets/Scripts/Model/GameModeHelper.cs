using System.Collections.Generic;

namespace Model
{
    public static class GameModeHelper
    {
        private static readonly Dictionary<GameMode, uint> FieldSizes = new Dictionary<GameMode, uint>
        {
            { GameMode.Easy, 6 },
            { GameMode.Hard, 9 },
        };

        public static uint FieldWidth(this GameMode gameMode) => FieldSizes[gameMode];
    }
}
