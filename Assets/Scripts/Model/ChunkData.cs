using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public struct ChunkData
    {
        public static readonly float Width = 1.5f;
        public static readonly float Height = 0.6f;

        public Vector3Int position;
        public Color color;
    }
}
