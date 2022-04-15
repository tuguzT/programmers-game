using Model.Tile;
using UnityEngine;

namespace Field
{
    public class BaseColor : IColor
    {
        public Color UnityColor => Color.white;

        private BaseColor()
        {
        }

        public static readonly BaseColor Instance = new();
    }
}
