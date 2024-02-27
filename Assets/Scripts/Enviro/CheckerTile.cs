using System;
using Movement;
using UnityEngine;

public enum CheckerDirection
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward
}
public class CheckerTile : MonoBehaviour
{
    public CheckerDirection Direction;

    private Tile _tileParent;

    private void Start()
    {
        _tileParent = GetComponentInParent<Tile>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tile>())
        {
            Tile currentTile = other.GetComponent<Tile>();
            
            switch (Direction)
            {
                case CheckerDirection.Up:
                    _tileParent.UpTile = currentTile;
                    break;
                case CheckerDirection.Down:
                    _tileParent.DownTile = currentTile;
                    break;
                case CheckerDirection.Left:
                    _tileParent.LeftTile = currentTile;
                    break;
                case CheckerDirection.Right:
                    _tileParent.RightTile = currentTile;
                    break;
                case CheckerDirection.Forward:
                    _tileParent.ForwardTile = currentTile;
                    break;
                case CheckerDirection.Backward:
                    _tileParent.BackwardTile = currentTile;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Tile>())
        {
            Tile currentTile = other.GetComponent<Tile>();
            switch (Direction)
            {
                case CheckerDirection.Up:
                    _tileParent.UpTile = null;
                    break;
                case CheckerDirection.Down:
                    _tileParent.DownTile = null;
                    break;
                case CheckerDirection.Left:
                    _tileParent.LeftTile = null;
                    break;
                case CheckerDirection.Right:
                    _tileParent.RightTile = null;
                    break;
                case CheckerDirection.Forward:
                    _tileParent.ForwardTile = null;
                    break;
                case CheckerDirection.Backward:
                    _tileParent.BackwardTile = null;
                    break;
            }
        }
    }
}
