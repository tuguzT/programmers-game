using System;
using Field.Easy;
using Field.Hard;
using Model;

namespace Field
{
    public static class FieldHelper
    {
        public static IField Field(this GameMode gameMode)
        {
            return gameMode switch
            {
                GameMode.Easy => new EasyField(),
                GameMode.Hard => new HardField(),
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
        }
    }
}
