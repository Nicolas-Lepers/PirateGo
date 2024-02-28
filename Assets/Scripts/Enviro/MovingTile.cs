using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingTile : MonoBehaviour
{
    public List<Transform> Waypoints;
    public float Duration;

    private Transform _actualWaypoint;
    [SerializeField] private int _waypointIndex;

    private void Start()
    {
        _actualWaypoint = Waypoints[0];
        transform.position = _actualWaypoint.position;
        
        StartCoroutine(MovePlatform(Waypoints[_waypointIndex]));
    }

    private IEnumerator MovePlatform(Transform waypoint)
    {
        transform.DOMove(waypoint.position, Duration);
        yield return new WaitForSeconds(Duration);

        if (_waypointIndex < Waypoints.Count -1)
            _waypointIndex += 1;
        else
        {
            _waypointIndex = 0;
        }
        StartCoroutine(MovePlatform(Waypoints[_waypointIndex]));
    }
}