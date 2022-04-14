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

        public static readonly HardColor Red = new HardColor(new Color(247 / 255f, 64 / 255f, 103 / 255f));

        public static readonly HardColor Pink = new HardColor(new Color(230 / 255f, 96 / 255f, 201 / 255f));

        public static readonly HardColor Orange = new HardColor(new Color(246 / 255f, 151 / 255f, 85 / 255f));

        public static readonly HardColor Yellow = new HardColor(new Color(240 / 255f, 203 / 255f, 90 / 255f));
    }
}
