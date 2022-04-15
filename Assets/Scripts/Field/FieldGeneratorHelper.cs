using System;
using Field.Easy;
using Field.Hard;
using Model;

namespace Field
{
    public static class FieldGeneratorHelper
    {
        public static IFieldGenerator GetFieldGenerator(this GameMode gameMode)
        {
            return gameMode switch
            {
                GameMode.Easy => new EasyFieldGenerator(),
                GameMode.Hard => new HardFieldGenerator(),
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
        }
    }
}
