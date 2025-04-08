using UnityEngine;
using System.Collections.Generic;
using Assets.Project.Scripts;

public class ResultManager : Singleton<ResultManager>
{
    [SerializeField] private WinSequence winSequience;
    private List<BetSpace> betSpaces;
    public float totalBet { get; set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        betSpaces = new List<BetSpace>();
    }

    public void SetResult(int result)
    {
        float totalWin = 0;

        foreach (BetSpace betSpace in betSpaces)
        {
            totalWin += betSpace.ResolveBet(result);
        }

        BalanceManager.Instance.ChangeBalance(totalWin);

        winSequience.ShowResult(result, totalWin);

        totalBet = 0;
        RouletteSceneManager.Instance.UpdateLocalPlayerText();

        ChipManager.Instance.EnableChips(true);
    }
   

    public void RegisterBetSpace(BetSpace betSpace)
    {
        betSpaces.Add(betSpace);
    }
}