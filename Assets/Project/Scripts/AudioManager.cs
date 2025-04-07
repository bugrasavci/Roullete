using System.Collections.Generic;
using Assets.Project.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private int maxAudioSourcePool = 15;
    [SerializeField] private AudioEvent[] audioEvent;
    [SerializeField] private AudioSource AudioSourceBGM;

    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle musicToggle;

    [SerializeField] private SettingsSavedData settingsSavedData;

    private List<AudioSource> audioSourcePool;
    private AudioSource auxiliarAS;
  

    protected override void Awake()
    {
        base.Awake();   
        AllocateAudioSources();
    }

    private void Start()
    {
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        soundToggle.onValueChanged.AddListener(ToggleSound);
        musicToggle.isOn = !settingsSavedData.MusicVolume;
        soundToggle.isOn = !settingsSavedData.SoundVolume;
    }

    public void ToggleSound(bool value)
    {
        settingsSavedData.SoundVolume = value ? false : true;
        if (auxiliarAS)
            auxiliarAS.volume = settingsSavedData.SoundVolume ? 1f : 0f;
    }
    public void ToggleMusic(bool value)
    {
        settingsSavedData.MusicVolume = value ? false : true;
        if (AudioSourceBGM)
            AudioSourceBGM.volume = settingsSavedData.MusicVolume ? 1f : 0f;
    }

    public void AllocateAudioSources()
    {
        audioSourcePool = new List<AudioSource>(maxAudioSourcePool);

        for (int i = 0; i < maxAudioSourcePool; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.loop = false;
            audioSourcePool.Add(source);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        var source = audioSourcePool[0];
        audioSourcePool.RemoveAt(0);
        audioSourcePool.Add(source);
        return source;
    }

    public void PlaySound(int iType)
    {
        var source = GetAvailableAudioSource();
        if (iType == 2)
            auxiliarAS = source;
        audioEvent[iType].PlayIn(source, settingsSavedData.SoundVolume ? 1f : 0f);
    }

    public void StopAuxiliarSound()
    {
        auxiliarAS.Stop();
    }    
}
