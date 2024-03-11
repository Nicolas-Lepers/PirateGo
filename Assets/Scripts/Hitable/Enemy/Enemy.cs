using DG.Tweening;
using Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{

    public enum Behaviour { Static, Rotating, Moving }

    [SerializeField] Behaviour _behaviourState;
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

        switch (_behaviourState)
        {
            case Behaviour.Rotating:
                {
                    float valueRotate = _rotateLeft == true ? 90 : -90;

                    var rotation = this.transform.eulerAngles;
                    rotation.y += valueRotate;
                    this.transform.eulerAngles = rotation;
                }
                break;
            case Behaviour.Moving:
                {
                    StartCoroutine(MoveAndRotate(_moveTime));
                }
                break;
        }
    }
    private IEnumerator MoveAndRotate(float time)
    {
        _path[_currentPath].gameObject.GetComponent<Tile>().SetHasEnemy(false);

        _currentPath = (_currentPath + 1) % _path.Count;

        var target = _path[_currentPath];
        _path[_currentPath].gameObject.GetComponent<Tile>().SetHasEnemy(true);

        transform.DOMove(target.position + _offsetPosition, _moveTime);

        yield return new WaitForSeconds(time);

        var nextTarget = _path[(_currentPath + 1) % _path.Count];

        transform.LookAt(nextTarget);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);
    }
    public void Hit()
    {
        Timer timer = this.gameObject.AddComponent<Timer>();
        StartCoroutine(timer.Execute(1, Disable));
        Debug.Log("hit enemy");
    }
    private void Disable()
    {
        GameManager.Instance.Enemies.Remove(this);
        this.gameObject.SetActive(false);
        _path[_currentPath].gameObject.GetComponent<Tile>().SetHasEnemy(false);

    }
}
