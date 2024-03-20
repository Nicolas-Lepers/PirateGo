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

    [HideInInspector] public List<Enemy> Enemies = new List<Enemy>();

    [SerializeField] int _targetFPS = 30;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = _targetFPS;
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



    void Update()
    {
        if (Application.targetFrameRate != _targetFPS)
            Application.targetFrameRate = _targetFPS;
    }
}
