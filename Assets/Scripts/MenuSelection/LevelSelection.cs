using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] Button _buttonSelection;

    public GameObject PreviousPath;
    public GameObject NextPath;
    [SerializeField] GameObject[] _stars;

    [SerializeField] AudioClip _selectNiveau;
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
        AudioManager.Instance.PlaySFXSound(_selectNiveau);
        SceneManager.LoadScene(levelName);
    }

    public void OnSelectLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
