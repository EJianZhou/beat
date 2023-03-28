using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMgr : MonoBehaviour
{
    public AudioSource[] bgmAudio, effectAudio, beatAudio, talkAudio;
    public AudioMixer masterMixer, bgmMixer, effectMixer, beatMixer, talkMixer;

    private static AudioMgr _instance;
    public static AudioMgr Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetMasterVolume(float value)
    {
        bgmMixer.SetFloat("masterMixer", value * 25 - 20);
    }

    public void SetBgmVolume(float value)
    {
        bgmMixer.SetFloat("bgmMixer", value * 25 - 20);
    }

    public void SetEffectVolume(float value)
    {
        effectMixer.SetFloat("effectMixer", value * 25 - 20);
    }

    public void SetBeatVolume(float value)
    {
        beatMixer.SetFloat("beatMixer", value * 25 - 20);
    }

    public void SetTalkVolume(float value)
    {
        talkMixer.SetFloat("talkMixer", value * 25 - 20);
    }

    public void PlayBgm(int id)
    {
        bgmAudio[id].loop = true;
        bgmAudio[id].Play();
    }

    public void PlayEffect(int id)
    {
        effectAudio[id].loop = false;
        effectAudio[id].Play();
    }

    public void PlayBeat(int id)
    {
        beatAudio[id].loop = true;
        beatAudio[id].Play();
    }

    public void PlayTalk(int id)
    {
        talkAudio[id].loop = false;
        talkAudio[id].Play();
    }

}
