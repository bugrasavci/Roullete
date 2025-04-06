using System.Collections;
using System.Collections.Generic;
using Assets.Project.Scripts;
using UnityEngine;

public class BetPool : Singleton<BetPool>
{
    private Stack<BetFootprint> betHistory = new();
    private List<BetSpace> activeBets = new();
    private List<BetSpace> rebetSnapshot = new();

    public void ResetStatus()
    {
        betHistory.Clear();
        rebetSnapshot = new List<BetSpace>(activeBets);
        activeBets.Clear();
    }

    public void AddBet(BetSpace space, float value)
    {
        if (space == null) return;

        betHistory.Push(new BetFootprint(space, value));

        if (!activeBets.Contains(space))
            activeBets.Add(space);
    }

    public void ClearAll()
    {
        foreach (var bet in activeBets)
            bet.Clear();

        betHistory.Clear();
        activeBets.Clear();

        RouletteSceneManager.Instance.UpdateUIInteractable(false);
        ResultManager.Instance.totalBet = 0;
    }

    public void Undo()
    {
        if (betHistory.Count == 0) return;

        var lastBet = betHistory.Pop();
        lastBet.betSpace.RemoveBet(lastBet.value);

        if (lastBet.betSpace.GetValue() <= 0f)
            activeBets.Remove(lastBet.betSpace);

        RouletteSceneManager.Instance.UpdateUIInteractable(betHistory.Count > 0);
    }
    public void RebetRoutine()
    {
        StartCoroutine(Rebet());
    }
    private IEnumerator Rebet()
    {
        ResultManager.Instance.totalBet = 0;
        AudioManager.Instance.PlaySound(3);

        foreach (var bet in rebetSnapshot)
        {
            bet.Rebet();
            yield return null;
        }

        RouletteSceneManager.Instance.UpdateUIInteractable(true);
    }


}

[System.Serializable]
public class BetFootprint
{
    public BetSpace betSpace;
    public float value;

    public BetFootprint(BetSpace betSpace, float value)
    {
        this.betSpace = betSpace;
        this.value = value;
    }
}
