using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{

    public void Execute()
    {
        Timer timer = new Timer();
        StartCoroutine(timer.Disable(1, this.gameObject));
        Debug.Log("hit enemy");
    }
}
