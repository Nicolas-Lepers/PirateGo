using UnityEngine;

public enum TileType
{
    Ground,
    Wall,
    WallGround
}

namespace Movement
{
    public class Tile : MonoBehaviour
    {
        [Header("Tile Type")] public TileType Type;

        [Header("Properties")] public bool IsAccessible;
        public bool HasEnemy;
        public bool HasLever;

        [Header("Origin")] public Transform Origin;

        [Header("Neightbor Tiles")] public Tile ForwardTile;
        public Tile BackwardTile;
        public Tile LeftTile;
        public Tile RightTile;

        [Header("Wall Tiles Properties")]
        public Tile UpTile;
        public Tile DownTile;


        public void SetHasEnemy(bool value)
        {
            HasEnemy = value;
            IsAccessible = !value;
        }

    }
}