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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevelStatus();
        UpdateVisualLevelSelection();
    }

    private void UpdateLevelStatus()
    {
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        if(PlayerPrefs.GetInt("Lv"+ previousLevelNum.ToString()) > 0)
        {
            _buttonSelection.interactable = true;
        }


        for (int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
        {
            _stars[i].GetComponent<Image>().color = Color.yellow;
        }
    }
    private void UpdateVisualLevelSelection()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].SetActive(_buttonSelection.IsInteractable());
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
