using Model.Tile;
using UnityEngine;

namespace Field.Easy
{
    public class EasyColor : IColor
    {
        public Color UnityColor { get; }

        private EasyColor(Color unityColor)
        {
            UnityColor = unityColor;
        }

        public static readonly EasyColor Yellow = new(new Color32(253, 208, 2, byte.MaxValue));

        public static readonly EasyColor Green = new(new Color32(172, 199, 44, byte.MaxValue));

        public static readonly EasyColor DarkGreen = new(new Color32(2, 168, 112, byte.MaxValue));
    }
}
