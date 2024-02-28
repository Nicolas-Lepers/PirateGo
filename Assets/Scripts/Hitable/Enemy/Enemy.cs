using DG.Tweening;
using Movement;
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
    public bool DoAction = true;

    [SerializeField, Tooltip("For the time of move animation")] float _moveTime;
    private void Start()
    {
        if (_currentPath <= _path.Count)
        {
            _path[_currentPath].gameObject.GetComponent<Tile>().SetHasEnemy(true);
            _offsetPosition = this.transform.position - _path[_currentPath].position;
        }

        GameManager.Instance.Enemies.Add(this);
    }

    public void Move()
    {
        if (DoAction == false)
        {
            return;
        }

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
                    _path[_currentPath].gameObject.GetComponent<Tile>().SetHasEnemy(false);

                    _currentPath = (_currentPath + 1) % _path.Count;

                    var target = _path[_currentPath];
                    _path[_currentPath].gameObject.GetComponent<Tile>().SetHasEnemy(true);

                    transform.DOMove(target.position + _offsetPosition, _moveTime);

                    StartCoroutine(RotateVisual(_moveTime));
                }
                break;
        }
    }
    private IEnumerator RotateVisual(float time)
    {
        yield return new WaitForSeconds(time);
        var nextTarget = _path[(_currentPath + 1) % _path.Count];

        transform.LookAt(nextTarget);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);
    }
    public void Execute()
    {
        Timer timer = this.gameObject.AddComponent<Timer>();
        StartCoroutine(timer.Execute(1, Disable));
        Debug.Log("hit enemy");
    }
    private void Disable()
    {
        this.gameObject.SetActive(false);
        _path[_currentPath].gameObject.GetComponent<Tile>().SetHasEnemy(false);

    }
}
