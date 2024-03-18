using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FOVEnemy : MonoBehaviour
{
    [SerializeField] Enemy _enemy;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _enemy.DoAction = false;
            GameManager.Instance.Player.AnimatorRef.SetTrigger("death");
            Debug.Log("player dead");
            //GameManager.Instance.SceneManagerRef.ResetScene();
        }
    }
}
