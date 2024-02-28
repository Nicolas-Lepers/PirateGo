using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHitable
{
    public void Execute()
    {
        Debug.Log("it's a target");
    }
}
