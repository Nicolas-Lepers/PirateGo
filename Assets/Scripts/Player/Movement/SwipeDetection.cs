using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float _minimumDistance = 0.2f;
    [SerializeField] private float _maximumTime = 1f;
    [SerializeField] private float _directionThreshold = 1;

    private InputManager _inputManager;

    private Vector2 _startPosition;
    private float _startTimer;

    private Vector2 _endPosition;
    private float _endTimer;

    private void Awake()
    {
        if (_inputManager == null)
        {
            _inputManager = InputManager.Instance;
        }
        else
        {
            Debug.LogError("There is already another Input Manager in this scene !");
        }
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTimer = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTimer = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance((Vector2)_startPosition, (Vector2)_endPosition) >= _minimumDistance &&
            (_endTimer - _startTimer) <= _maximumTime)
        {
            Debug.Log("Swipe Detected");
            Debug.DrawLine((Vector2)_startPosition, (Vector2)_endPosition, Color.red, 5f);
            Vector2 direction2D = ((Vector2)_endPosition- (Vector2)_startPosition).normalized;
            PlayerController.Instance.SwipeDirection(direction2D, _directionThreshold);
        }
    }
}