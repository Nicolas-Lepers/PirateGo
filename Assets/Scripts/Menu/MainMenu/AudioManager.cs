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
    public AudioSource MasterSound;

    public AudioClip[] AudioClips;

    public Coroutine CoroutineMusic;
    public Coroutine CoroutineSFX;
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
    public void SetMasterVolume(Toggle toggle)
    {
        Mixer.SetFloat("MasterVolume", Mathf.Log10(toggle.isOn == true ? 0.0001f : 1) * 20);
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
    public void PlaySFXSound(AudioClip clip)
    {
        AudioSourceSFX.PlayOneShot(clip);
    }

    public IEnumerator IEPlayMusicSound(string name)
    {
        AudioClip clip = GetClip(name);
        AudioSourceMusic.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        CoroutineMusic = StartCoroutine(IEPlayMusicSound(name));
    }

    public IEnumerator IEPlayMusicSound(AudioClip clip)
    {
        AudioSourceMusic.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        CoroutineMusic = StartCoroutine(IEPlayMusicSound(clip));
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
