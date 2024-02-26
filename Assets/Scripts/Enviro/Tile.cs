using UnityEngine;

namespace Movement
{
    public class Tile : MonoBehaviour
    {
        public bool IsAccessible;
        public bool HasEnemy;
        public bool HasLever;

        [Header("Neightbor Tiles")] 
        public Tile ForwardTile;
        public Tile BackwardTile;
        public Tile LeftTile;
        public Tile RightTile;
 
        public Tile UpTile;
        public Tile DownTile;
    }
}