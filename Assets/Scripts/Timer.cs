using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public IEnumerator Execute(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();

        Destroy(this);
    }
}
