using UnityEngine;

namespace Model.Tile
{
    public sealed class CarData : ITile
    {
        public Vector3Int Position { get; }
        public Direction Direction { get; }
        public TeamColor TeamColor { get; }

        public CarData(BaseData baseData)
        {
            var position = baseData.Position;
            position.y += 1;
            Position = position;
            Direction = baseData.Direction;
            TeamColor = baseData.TeamColor;
        }

        public CarData(Vector3Int position, Direction direction, TeamColor color)
        {
            Position = position;
            Direction = direction;
            TeamColor = color;
        }
    }
}
