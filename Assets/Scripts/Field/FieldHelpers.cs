using System;
using Model;

namespace Field
{
    public static class FieldHelpers
    {
        public static IField Field(this GameMode gameMode) => gameMode switch
        {
            GameMode.Easy => new EasyField(),
            GameMode.Hard => new HardField(),
            _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
        };
    }
}
