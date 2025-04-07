using System;
using Assets.Project.Scripts.SaveSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsSavedData", menuName = "SavedObject/SettingsSavedData", order = 0)]

public class SettingsSavedData : SavedObjectBaseSO<SettingsSavedObjectData>
{
    public bool SoundVolume
    {
        get => SavedData.soundVolume;
        set
        {
            SavedData.soundVolume = value;
            SaveManual();
        }
    }
    public bool MusicVolume
    {
        get => SavedData.musicVolume;
        set
        {
            SavedData.musicVolume = value;
            SaveManual();
        }
    }
}
[Serializable]
public class SettingsSavedObjectData
{
    public bool soundVolume;
    public bool musicVolume;


}
