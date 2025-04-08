using System;
using Assets.Project.Scripts.GameEvents;
using Assets.Project.Scripts.SaveSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "HistoricalRecordSavedData", menuName = "SavedObject/HistoricalRecordSavedData", order = 0)]
public class HistoricalRecordSavedData : SavedObjectBaseSO<HistoricalRecordSavedObjectData>
{
    [SerializeField] private GameEvent onChangeHistoricalRecord;
    public int TotalSpin
    {
        get => SavedData.totalSpin;
        set
        {
            SavedData.totalSpin = value;
            SaveManual();
            onChangeHistoricalRecord.Invoke();
        }
    }
    public int TotalWinnings
    {
        get => SavedData.totalWinnings;
        set
        {
            SavedData.totalWinnings = value;
            SaveManual();
            onChangeHistoricalRecord.Invoke();
        }
    }
    public int TotalLosses
    {
        get => SavedData.totalLosses;
        set
        {
            SavedData.totalLosses = value;
            SaveManual();
            onChangeHistoricalRecord.Invoke();
        }
    }
    public int NetProfit => TotalWinnings - TotalLosses;

}
[Serializable]
public class HistoricalRecordSavedObjectData
{
    public int totalSpin;
    public int totalWinnings;
    public int totalLosses;
}
