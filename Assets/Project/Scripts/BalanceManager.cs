using Assets.Project.Scripts;
using UnityEngine;

public class BalanceManager : Singleton<BalanceManager>
{
    [SerializeField] BalanceSavedData balanceSavedData;

    public float GetBalence => balanceSavedData.Balance;
    private void Start()
    {
        NotifyBalanceChanged();
    }  

    public void ChangeBalance(float value)
    {
        balanceSavedData.Balance += value;
        NotifyBalanceChanged();
    }

    private void NotifyBalanceChanged()
    {
        RouletteSceneManager.Instance.UpdateLocalPlayerText();
    }
}
