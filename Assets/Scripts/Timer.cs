using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public IEnumerator Disable(float time,GameObject go)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);

        Destroy(this);
    }
}
