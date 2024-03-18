using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioSource AudioSourceMusic;
    public AudioSource AudioSourceSFX;

    public AudioClip[] AudioClips;

    [HideInInspector] public bool Coroutine = false;
    //public float step = 1f;

    public float MusicSliderValue;
    public float SfxSliderValue;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SetMusicLevel(MusicSliderValue);
        SetSFXLevel(SfxSliderValue);
    }

    public void SetMusicLevel(float sliderValue)
    {
        MusicSliderValue = sliderValue;
        Mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSFXLevel(float sliderValue)
    {
        SfxSliderValue = sliderValue;
        Mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void PlayMusicSound(string name)
    {
        AudioClip clip = GetClip(name);
        AudioSourceMusic.PlayOneShot(clip);
    }
    public void PlaySFXSound(string name)
    {
        AudioClip clip = GetClip(name);
        AudioSourceSFX.PlayOneShot(clip);
    }

    public IEnumerator IEPlayMusicSound(string name)
    {
        AudioClip clip = GetClip(name);
        AudioSourceMusic.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        StartCoroutine(IEPlayMusicSound(name));
    }

    //public IEnumerator Step(string name)
    //{
    //    AudioClip clip = GetClip(name);
    //    AudioSourceSFX.PlayOneShot(clip);
    //    yield return new WaitForSeconds(clip.length);
    //    StartCoroutine(Step(name));
    //}

    AudioClip GetClip(string name)
    {
        foreach (var item in AudioClips)
        {
            if (item.name == name)
                return item;
        }
        return null;
    }
}
