using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ManagerUI ManagerUIRef;

    public MySceneManager SceneManagerRef;

    public Bullet BulletObject;

    public PlayerAttack PlayerTempRef;
    public PlayerController Player;

    [HideInInspector]public List<Enemy> Enemies = new List<Enemy>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Instance already exist");
        }
    }


    public void MoveGame()
    {
        #region Enemy
        foreach (var enemy in Enemies)
        {
            enemy.Move();
        }
        #endregion
    }
}
