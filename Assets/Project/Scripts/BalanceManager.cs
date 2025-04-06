using Assets.Project.Scripts;
using UnityEngine;

public class BalanceManager : Singleton<BalanceManager>
{
    public float Balance { get; private set; } = 0;
    [SerializeField] float StartValue;

    private void Start()
    {
        SetBalance(StartValue);
    }
    public void SetBalance(float balance)
    {
        Balance = balance;
        NotifyBalanceChanged();
    }

    public void ChangeBalance(float value)
    {
        Balance += value;
        NotifyBalanceChanged();
    }

    public void ResetBalance()
    {
        SetBalance(StartValue);
    }

    private void NotifyBalanceChanged()
    {
        RouletteSceneManager.Instance.UpdateLocalPlayerText();
    }
}
