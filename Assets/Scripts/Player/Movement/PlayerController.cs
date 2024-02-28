using System.Collections;
using UnityEngine;
using DG.Tweening;
using Movement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Properties")]
    [SerializeField] private bool _canMove;
    [SerializeField] private bool _isMoving;

    [Header("Tile")] public Tile InitialTile;
    private Tile _currentTile;

    [Header("Movement Delay")] [SerializeField]
    private float _groundMoveDelay;

    [SerializeField] private float _wallMoveDelay;
    [SerializeField] private float _wallGroundMoveDelay;

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
        _currentTile = InitialTile;
    }

    private void Update()
    {
        _canMove = !_isMoving;

        if (_isMoving == false)
            transform.position = _currentTile.Origin.transform.position;
    }

    #region Swipe Direction

    public void SwipeDirection(Vector2 direction, float _directionThreshold)
    {
        switch (_currentTile.Type)
        {
            case TileType.Ground:
                SetNewTile(SwipeFromGround(direction, _directionThreshold));
                break;
            case TileType.Wall:
                SetNewTile(SwipeFromWall(direction, _directionThreshold));
                break;
            case TileType.WallGround:
                SetNewTile(SwipeFromWallGround(direction, _directionThreshold));
                break;
        }
    }

    private Tile SwipeFromGround(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            return _currentTile.ForwardTile;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            return _currentTile.BackwardTile;
        }

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            return _currentTile.LeftTile;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            return _currentTile.RightTile;
        }

        return null;
    }

    private Tile SwipeFromWall(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            return _currentTile.UpTile;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            return _currentTile.DownTile != null ? _currentTile.DownTile : _currentTile.BackwardTile;
        }

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            return _currentTile.LeftTile;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            return _currentTile.RightTile;
        }

        return null;
    }

    private Tile SwipeFromWallGround(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            return _currentTile.ForwardTile;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            return _currentTile.BackwardTile;
        }

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            return _currentTile.LeftTile;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            return _currentTile.DownTile;
        }

        return null;
    }

    #endregion

    #region Move

    private void SetNewTile(Tile newTile)
    {
        if (newTile == null) return;
        if (newTile.IsAccessible == false) return;
        if(_canMove == false) return;

        _currentTile = newTile;

        StartCoroutine(MoveToTile(newTile));
    }

    public float moveDelay = 0f;

    private IEnumerator MoveToTile(Tile newTile)
    {
        _isMoving = true;

        //float moveDelay = 0f;

        switch (newTile.Type)
        {
            case TileType.Ground:
                moveDelay = _groundMoveDelay;
                break;
            case TileType.Wall:
                moveDelay = _wallMoveDelay;
                break;
            case TileType.WallGround:
                moveDelay = _wallGroundMoveDelay;
                break;
        }

        transform.DOMove(newTile.Origin.position, moveDelay);
        yield return new WaitForSeconds(moveDelay);

        _isMoving = false;
    }

    #endregion
}