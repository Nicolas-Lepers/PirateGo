using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{

    public enum Behaviour { Static, Rotating, Moving }

    public Behaviour BehaviourState;
    [SerializeField] bool _rotateLeft = false;

    [SerializeField] List<Transform> _path = new List<Transform>();
    private int _currentPath = 0;
    private Vector3 _offsetPosition;
    private void Start()
    {
        if (_currentPath < _path.Count)
        {
            _offsetPosition = this.transform.position - _path[_currentPath].position;
        }

        GameManager.Instance.Enemies.Add(this);
    }

    public void Move()
    {
        switch (BehaviourState)
        {
            case Behaviour.Rotating:
                {
                    if (_rotateLeft == true)
                    {
                        var rotation = this.transform.eulerAngles;
                        rotation.y += 90;
                        this.transform.eulerAngles = rotation;
                    }
                    else
                    {
                        var rotation = this.transform.eulerAngles;
                        rotation.y -= 90;
                        this.transform.eulerAngles = rotation;
                    }
                }
                break;
            case Behaviour.Moving:
                {
                    _currentPath = (_currentPath + 1) % _path.Count;

                    var target = _path[_currentPath];
                    var position = this.transform.position;
                    position = target.position;
                    position += _offsetPosition;

                    this.transform.position = position;


                    var nextTarget = _path[(_currentPath + 1) % _path.Count];

                    transform.LookAt(nextTarget);
                    Vector3 eulerAngles = transform.rotation.eulerAngles;
                    eulerAngles.x = 0;
                    eulerAngles.z = 0;
                    transform.rotation = Quaternion.Euler(eulerAngles);

                }
                break;
        }
    }

    public void Execute()
    {
        Timer timer = this.gameObject.AddComponent<Timer>();
        StartCoroutine(timer.Disable(1, this.gameObject));
        Debug.Log("hit enemy");
    }
}
