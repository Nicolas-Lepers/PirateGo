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
            if (_enemy.CoroutineMove != null)
            {
                StopCoroutine(_enemy.CoroutineMove);
            }

            _enemy.DoAction = false;
            _enemy.AnimatorRef.SetTrigger("attack");


            GameManager.Instance.Player.SetDead(true);
            GameManager.Instance.Player.AnimatorRef.SetTrigger("death");
            StartCoroutine(player.gameObject.AddComponent<Timer>().Execute(2, GameManager.Instance.SceneManagerRef.ResetScene));
        }
    }
}
