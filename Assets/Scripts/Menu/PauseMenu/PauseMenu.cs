using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //[SerializeField] MainGame _mainGame;
    
    public GameObject AccueilPause;
    public GameObject Parameter;
    public GameObject Controls;
    public GameObject Credits;

    void Awake()
    {
        AccueilPause.SetActive(false);
        Parameter.SetActive(false);
        Controls.SetActive(false);
        Credits.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Parameter.activeInHierarchy == false &&
                Controls.activeInHierarchy == false &&
                Credits.activeInHierarchy == false)
            {
                AccueilPause.SetActive(!AccueilPause.activeSelf);
                //_mainGame.Pause = AccueilPause.activeSelf;
            }
        }
    }
    public void OnClickResume()
    {
        AccueilPause.SetActive(false);
        //_mainGame.Pause = false;
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickParameter()
    {
        AccueilPause.SetActive(false);
        Parameter.SetActive(true);
    }
    public void OnClickCredits()
    {
        AccueilPause.SetActive(false);
        Credits.SetActive(true);
    }
    public void OnClickControls()
    {
        Controls.SetActive(true);
        Parameter.SetActive(false);
    }
    public void OnClickReturn()
    {
        if (Controls.activeInHierarchy == true)
        {
            Controls.SetActive(!Controls);
            Parameter.SetActive(true);
        }
        else
        {
            AccueilPause.SetActive(true);
            Credits.SetActive(false);
            Parameter.SetActive(false);
        }
    }
    public void OnClickLeave()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
