using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] Button _buttonSelection;
    public Button ButtonSelection => _buttonSelection;
    public GameObject PreviousPath;
    public GameObject NextPath;
    [SerializeField] GameObject[] _stars;


    void Update()
    {
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus()
    {
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        if(PlayerPrefs.GetInt("Lv"+ previousLevelNum.ToString()) > 0)
        {
            _buttonSelection.interactable = true;
        }
    }

    public void OnSelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void OnSelectLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
