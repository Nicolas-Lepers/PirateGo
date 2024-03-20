using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [SerializeField] AudioClip _musicMenu;
    [SerializeField] AudioClip _musicLevel;
    [SerializeField] AudioClip _sfxEndLvl;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetScene()
    {
        StopCoroutine(AudioManager.Instance.CoroutineMusic);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.CoroutineMusic = StartCoroutine(AudioManager.Instance.IEPlayMusicSound(_musicLevel));
    }
    public void SceneSelectStage()
    {
        StopCoroutine(AudioManager.Instance.CoroutineMusic);
        SceneManager.LoadScene("MenuSelection");
        AudioManager.Instance.CoroutineMusic = StartCoroutine(AudioManager.Instance.IEPlayMusicSound(_musicMenu));
    }
    [SerializeField] int _levelIndex;
    public void OnEndLevel(int stars)
    {
        AudioManager.Instance.PlaySFXSound(_sfxEndLvl);
        PlayerPrefs.SetInt("Lv" + _levelIndex, stars);
    }
}
