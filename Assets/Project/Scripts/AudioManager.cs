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

    private List<AudioSource> audioSourcePool;
    private AudioSource auxiliarAS;

    private float soundVolume = 1;
    private float musicVolume = 0.4f;

    protected override void Awake()
    {
        base.Awake();   
        AllocateAudioSources();
    }

    private void Start()
    {
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        soundToggle.onValueChanged.AddListener(ToggleSound);
    }

    public void ToggleSound(bool value)
    {
        soundVolume = value ? 0 : 1;
        if (auxiliarAS)
            auxiliarAS.volume = soundVolume;
    }
    public void ToggleMusic(bool value)
    {
        musicVolume = value ? 0 : 1;
        if (AudioSourceBGM)
            AudioSourceBGM.volume = musicVolume;
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
        audioEvent[iType].PlayIn(source, soundVolume);
    }

    public void StopAuxiliarSound()
    {
        auxiliarAS.Stop();
    }    
}
