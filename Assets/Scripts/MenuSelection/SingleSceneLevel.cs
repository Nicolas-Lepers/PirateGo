using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleSceneLevel : MonoBehaviour
{

    public int LevelIndex;
    public void OnReturnMenuSelection()
    {
        SceneManager.LoadScene("MenuSelection");

    }
    int _currentStars = 0;
    public void OnSelectStars(int stars)
    {
        _currentStars = stars;
        if (_currentStars > PlayerPrefs.GetInt("Lv" + LevelIndex))
        {
            PlayerPrefs.SetInt("Lv" + LevelIndex, _currentStars);
        }
        //OnReturnMenuSelection();
        Debug.Log(LevelIndex +" " + PlayerPrefs.GetInt("Lv" + LevelIndex, _currentStars));
    }
}
