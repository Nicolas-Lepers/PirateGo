using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _currentLevel;
    [SerializeField] GameObject _firstLevel;

    void Start()
    {
        //PlayerPrefs.DeleteAll();

        _currentLevel = GameObject.Find(PlayerPrefs.GetString("CurrentLvl")) == null ?
            _firstLevel : GameObject.Find(PlayerPrefs.GetString("CurrentLvl"));


    }

}
