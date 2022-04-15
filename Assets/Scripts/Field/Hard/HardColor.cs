using Model.Tile;
using UnityEngine;

namespace Field.Hard
{
    public class HardColor : IColor
    {
        public Color UnityColor { get; }

        private HardColor(Color unityColor)
        {
            UnityColor = unityColor;
        }

        public static readonly HardColor Red = new(new Color32(247, 64, 103, byte.MaxValue));

        public static readonly HardColor Pink = new(new Color32(230, 96, 201, byte.MaxValue));

        public static readonly HardColor Orange = new(new Color32(246, 151, 85, byte.MaxValue));

        public static readonly HardColor Yellow = new(new Color32(240, 203, 90, byte.MaxValue));
    }
}
