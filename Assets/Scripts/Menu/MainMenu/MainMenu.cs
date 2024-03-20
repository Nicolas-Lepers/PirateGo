using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{

    //public Image imageFade;

    public GameObject Title;

    public GameObject Accueil;
    public GameObject Parameter;
    public GameObject Controls;
    public GameObject Credits;

    [SerializeField] AudioClip _musicMenu;

    //public List<Interface> buttons;
    private void Update()
    {
        if (Accueil.gameObject.activeInHierarchy == true)
            Title.SetActive(true);
        else
            Title.SetActive(false);

    }
    private void Awake()
    {
        Accueil.SetActive(true);
        Parameter.SetActive(false);
        Controls.SetActive(false);
        Credits.SetActive(false);
    }
    private void Start()
    {
        AudioManager.Instance.CoroutineMusic = StartCoroutine(AudioManager.Instance.IEPlayMusicSound(_musicMenu));

    }
    public void OnClickPlay()
    {

        //imageFade.DOFade(1, 0.8f).OnComplete(FadePlayComplete);
        //for (int i = 1; i < buttons.Count; i++)
        //{
        //    buttons[i].Hide(0.1f);
        //}//Polish pas la
        FadePlayComplete();//a tej si tu remet au dessus


    }

    void FadePlayComplete()
    {
        StopCoroutine(AudioManager.Instance.CoroutineMusic);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickParameter()
    {
        Accueil.SetActive(false);
        Parameter.SetActive(true);
    }
    public void OnClickCredits()
    {
        Accueil.SetActive(false);
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
            Accueil.SetActive(true);
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
