using UnityEngine;
using Movement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public Tile InitialTile;
    public Tile CurrentTile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is another Player Controller in this scene !");
        }
    }

    private void Start()
    {
        transform.position = InitialTile.Origin.position;
        CurrentTile = InitialTile;
    }

    private void Update()
    {
        transform.position = CurrentTile.Origin.transform.position;
    }

    public void SwipeDirection(Vector2 direction, float _directionThreshold)
    {
        switch (CurrentTile.Type)
        {
            case TileType.Ground:
                Move(SwipeFromGround(direction, _directionThreshold));
                break;
            case TileType.Wall:
                Move(SwipeFromWall(direction, _directionThreshold));
                break;
            case TileType.WallGround:
                Move(SwipeFromWallGround(direction, _directionThreshold));
                break;
        }
    }
    
    private Tile SwipeFromGround(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
           return CurrentTile.ForwardTile;
        }
        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
           return CurrentTile.BackwardTile;
        }
        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            return CurrentTile.LeftTile;
        }
        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            return CurrentTile.RightTile;
        }

        return null;
    }
    private Tile SwipeFromWall(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
           return CurrentTile.UpTile;
        }
        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            return CurrentTile.DownTile != null ? CurrentTile.DownTile : CurrentTile.BackwardTile;
        }
        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
           return CurrentTile.LeftTile;
        }
        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
           return CurrentTile.RightTile;
        }

        return null;
    }
    private Tile SwipeFromWallGround(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
           return CurrentTile.ForwardTile;
        }
        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
           return CurrentTile.BackwardTile;
        }
        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            return CurrentTile.LeftTile;
        }
        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
          return CurrentTile.DownTile;
        }

        return null;
    }

    private void Move(Tile nextTile)
    {
        if (nextTile == null) return;
        
        transform.position = nextTile.Origin.position;
        CurrentTile = nextTile;

        GameManager.Instance.MoveGame();
    }
}