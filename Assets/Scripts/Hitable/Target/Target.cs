using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHitable
{
    public void Hit()
    {
        Debug.Log("it's a target");
    }
}
