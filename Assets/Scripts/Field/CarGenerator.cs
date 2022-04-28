using System.Collections.Generic;
using System.Linq;
using Model.Tile;
using UnityEngine;

namespace Field
{
    public static class CarGenerator
    {
        public static byte PlayerCount => GameManager.Instance.PlayerCount;

        public static IEnumerable<CarData> Generate(IEnumerable<BaseData> bases)
        {
            var chosenBases = bases
                .OrderBy(_ => Random.Range(0f, 1f))
                .Take(PlayerCount);
            var cars = chosenBases.Select(data => new CarData(data));
            return cars;
        }
    }
}
