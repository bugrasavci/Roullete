using System;
using Assets.Project.Scripts.SaveSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "BalanceSavedData", menuName = "SavedObject/BalanceSavedData", order = 0)]
public class BalanceSavedData : SavedObjectBaseSO<BalanceSavedObjectData>
{
    public float Balance
    {
        get => SavedData.balance;
        set
        {
            SavedData.balance = value;
            SaveManual();
        }
    }
}
[Serializable]
public class BalanceSavedObjectData
{
    public float balance = 0;


}
