using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SceneSelectStage()
    {
        SceneManager.LoadScene("MenuSelection");
    }
    [SerializeField] int _levelIndex;
    public void OnEndLevel(int stars)
    {
        PlayerPrefs.SetInt("Lv" + _levelIndex, stars);
    }
}
