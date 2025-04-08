using TMPro;
using UnityEngine;

public class HistoricalRecorPanelController : MonoBehaviour
{
    [SerializeField] private HistoricalRecordSavedData historicalRecordSavedData;
    [SerializeField] private TMP_Text totalSpinsText;
    [SerializeField] private TMP_Text totalWinningsText;
    [SerializeField] private TMP_Text totalLossesText;
    [SerializeField] private TMP_Text netProfitText;

    private void Start()
    {
        ChangeUI();
    }
    public void ChangeUI()
    {
        totalSpinsText.text = $"Total Spins: {historicalRecordSavedData.TotalSpin}";
        totalWinningsText.text = $"Total Winnings: ${historicalRecordSavedData.TotalWinnings}";
        totalLossesText.text = $"Total Losses: ${historicalRecordSavedData.TotalLosses}";
        netProfitText.text = $"Net Profit: ${historicalRecordSavedData.NetProfit}";

        ChangeColor(historicalRecordSavedData.NetProfit);
    }
    private void ChangeColor(int netProfit)
    {
        if (netProfit > 0)
            netProfitText.color = Color.green;
        else if (netProfit < 0)
            netProfitText.color = Color.red;
        else
            netProfitText.color = Color.white;
    }

}
