using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class Tile : MonoBehaviour
    {
        public List<Tile> NeighborTiles;

        public bool IsAccessible;
        public bool HasEnemy;
    }
}