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

        public static readonly EasyColor Yellow = new(new Color(253 / 255f, 208 / 255f, 2 / 255f));

        public static readonly EasyColor Green = new(new Color(172 / 255f, 199 / 255f, 44 / 255f));

        public static readonly EasyColor DarkGreen = new(new Color(2 / 255f, 168 / 255f, 112 / 255f));
    }
}
