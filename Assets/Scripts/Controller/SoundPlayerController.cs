using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerController : UnitySingleton<SoundPlayerController>
{
    // Start is called before the first frame update

    public int playBgmId = -1;
    public int playEffectId = -1;
    public int playTalkId = -1;
    public int playBeatId = -1;

    public float setMasterVolume = -10;
    public float setBgmVolume = -10;
    public float setEffectVolume = -10;
    public float setTalkVolume = -10;
    public float setBeatVolume = -10;

    // Update is called once per frame
    void Update()
    {
        if (playBgmId!=-1)
        {
            AudioMgr.Instance.PlayBgm(playBgmId);
            playBgmId = -1;
        }

        if (playEffectId != -1)
        {
            AudioMgr.Instance.PlayEffect(playEffectId);
            playEffectId = -1;
        }

        if (playTalkId != -1)
        {
            AudioMgr.Instance.PlayTalk(playTalkId);
            playTalkId = -1;
        }

        if (playBeatId != -1)
        {
            AudioMgr.Instance.PlayBeat(playBeatId);
            playBeatId = -1;
        }

        if (setBeatVolume>0)
        {
            AudioMgr.Instance.SetBeatVolume(setBeatVolume);
            setBeatVolume = -10;
        }

        if (setBgmVolume > 0)
        {
            AudioMgr.Instance.SetBgmVolume(setBgmVolume);
            setBgmVolume = -10;
        }

        if (setMasterVolume > 0)
        {
            AudioMgr.Instance.SetMasterVolume(setMasterVolume);
            setMasterVolume = -10;
        }

        if (setTalkVolume > 0)
        {
            AudioMgr.Instance.SetTalkVolume(setTalkVolume);
            setTalkVolume = -10;
        }

        if (setEffectVolume > 0)
        {
            AudioMgr.Instance.SetEffectVolume(setEffectVolume);
            setEffectVolume = -10;
        }





    }
}
