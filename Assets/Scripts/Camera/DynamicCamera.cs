using System;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform Target;
    public float SmoothSpeed = 0.01f;


    private Vector3 _offset;
    private void Start()
    {
        _offset = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = Target.position + _offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position,
            targetPos, SmoothSpeed * Time.deltaTime);

        transform.position = smoothFollow;
    }
}
