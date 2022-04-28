using System;
using Field.Easy;
using Field.Hard;
using Model;

namespace Field
{
    public static class FieldGeneratorHelper
    {
        public static IFieldGenerator GetFieldGenerator(this Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => new EasyFieldGenerator(),
                Difficulty.Hard => new HardFieldGenerator(),
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };
        }
    }
}
