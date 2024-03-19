using System.Collections;
using UnityEngine;
using DG.Tweening;
using Movement;
using System;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public static PlayerController Instance;

    [Header("Properties")]
    [SerializeField]
    private bool _canMove;
    private bool _atEnd = false;

    [SerializeField] private bool _isMoving;

    [Header("Tile")] public Tile InitialTile;
    private Tile _currentTile;

    [Header("Movement Delay")]
    [SerializeField]
    private float _groundMoveDelay;

    [SerializeField] private float _wallMoveDelay;
    [SerializeField] private float _wallGroundMoveDelay;
    private float _moveDelay;
    public Animator AnimatorRef;
    #endregion

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
        if (_atEnd == true) { return; }

        _canMove = !_isMoving;

        if (_isMoving == false)
            transform.position = _currentTile.Origin.transform.position;
    }

    #region Swipe Direction

    public void SwipeDirection(Vector2 direction, float _directionThreshold)
    {
        if (_atEnd == true) { return; }

        Debug.Log($"{direction} direction");

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
            Debug.Log($"up");


            return _currentTile.ForwardTile;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log($"down");
            return _currentTile.BackwardTile;
        }

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log($"left");
            return _currentTile.LeftTile;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log($"right");
            return _currentTile.RightTile;
        }

        return null;
    }

    private Tile SwipeFromWall(Vector2 direction, float _directionThreshold)
    {

        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            Debug.Log($"up");
            _moveWallVertical = true;
            return _currentTile.UpTile;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log($"down");
            _moveWallVertical = true;
            return _currentTile.DownTile != null ? _currentTile.DownTile : _currentTile.BackwardTile;
        }


        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log($"left");
            _moveWallVertical = false;
            return _currentTile.LeftTile;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log($"right");
            _moveWallVertical = false;
            return _currentTile.RightTile;
        }

        return null;
    }
    private bool _moveWallVertical = false;
    private Tile SwipeFromWallGround(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            Debug.Log($"up");
            return _currentTile.ForwardTile;
        }

        if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log($"down");
            return _currentTile.BackwardTile;
        }

        if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log($"left");
            return _currentTile.LeftTile;
        }

        if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log($"right");

            return _currentTile.DownTile;
        }

        return null;
    }

    #endregion

    #region Move

    private void SetNewTile(Tile newTile)
    {
        if (newTile == null || newTile.IsAccessible == false || _canMove == false) return;

        if (newTile.End == true)
        {
            _atEnd = newTile.End;
            StartCoroutine(gameObject.AddComponent<Timer>().Execute(1.5f, GameManager.Instance.SceneManagerRef.LoadNextScene));
        }

        Animate(newTile);

        _currentTile = newTile;


        RotatePlayer(newTile);

        StartCoroutine(MoveToTile(newTile));
    }

    private void Animate(Tile newTile)
    {
        switch (newTile.Type)
        {
            case TileType.Ground:
                {
                    if (_currentTile.Type == TileType.Wall)
                    {
                        AnimatorRef.SetBool("idle_grimp", false);
                        AnimatorRef.SetBool("idle", true);
                        AnimatorRef.SetTrigger("walk");
                        Debug.Log("transi wall ground");
                    }
                    else
                    {
                        AnimatorRef.SetTrigger("walk");
                    }

                }
                break;
            case TileType.Wall:
                {
                    if (_currentTile.Type == TileType.Ground)
                    {
                        AnimatorRef.SetTrigger("walk");
                        Debug.Log("transi ground wall");
                        AnimatorRef.SetBool("idle_grimp", true);
                        AnimatorRef.SetBool("idle", false);
                    }
                    else
                    {
                        if (_moveWallVertical == true)
                        {
                            AnimatorRef.SetTrigger("grimp");
                        }
                        else
                        {
                            Debug.Log("side");
                            //AnimatorRef.SetTrigger("grimp_side");
                        }
                    }
                }
                break;
        }
    }

    private IEnumerator MoveToTile(Tile newTile)
    {
        _isMoving = true;

        switch (newTile.Type)
        {
            case TileType.Ground:
                _moveDelay = _groundMoveDelay;
                break;
            case TileType.Wall:
                _moveDelay = _wallMoveDelay;
                break;
            case TileType.WallGround:
                _moveDelay = _wallGroundMoveDelay;
                break;
        }

        transform.DOMove(newTile.Origin.position, _moveDelay);
        yield return new WaitForSeconds(_moveDelay);

        GameManager.Instance.MoveGame();

        _isMoving = false;
    }

    #endregion

    #region Rotate

    private void RotatePlayer(Tile newTile)
    {
        if (newTile.Type == TileType.Ground || newTile.Type == TileType.WallGround)
        {
            transform.LookAt(new Vector3(newTile.Origin.transform.position.x, transform.position.y, newTile.Origin.transform.position.z));
        }
        else if (newTile.Type == TileType.Wall)
        {
            transform.LookAt(new Vector3(newTile.transform.position.x, transform.position.y, transform.position.z));
        }
    }

    #endregion
}