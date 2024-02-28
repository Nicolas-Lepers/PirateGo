using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            Debug.Log("player dead");
        }
    }
}
