using Assets.Project.Scripts;
using TMPro;
using UnityEngine;

public class BetLimitManager : Singleton<BetLimitManager>
{
    [SerializeField] private float max = 1000f;
    [SerializeField] private float min = 1f;

    [SerializeField] private TMP_Text minT;
    [SerializeField] private TMP_Text maxT;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        minT.text = $" Min: {min.ToString("0.0")}";
        maxT.text = $" Min: {max.ToString("0.0")}";
    }

    public void SetBetLimits(float minBet, float maxBet)
    {
        min = minBet;
        max = maxBet;
    }

    public bool AllowLimit(float value)
    {
        if (ResultManager.Instance.totalBet + value > max || ResultManager.Instance.totalBet + value < min)
        {
            return false;
        }
        return true;
    }
}
