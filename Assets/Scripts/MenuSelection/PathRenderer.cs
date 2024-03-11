using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] GameObject[] _paths;
    public GameObject[] Paths => _paths;
    [SerializeField] LineRenderer _lineRenderer;


    private void Start()
    {
        _lineRenderer.positionCount = _paths.Length;
    }
    private void Update()
    {
        for (int i = 0; i < _paths.Length; i++)
        {
            _lineRenderer.SetPosition(i, _paths[i].gameObject.transform.position);
        }
    }

}
