using System.Collections.Generic;

public static class GameModeHelpers
{
    private static readonly Dictionary<GameMode, uint> FieldSizes = new Dictionary<GameMode, uint>
    {
        { GameMode.Easy, 6 },
        { GameMode.Hard, 9 },
    };

    public static uint FieldSize(this GameMode gameMode) => FieldSizes[gameMode];
}
