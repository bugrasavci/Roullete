using UnityEngine;
using TMPro;
using System.Collections;
using Assets.Project.Scripts.GameEvents;
using System.Collections.Generic;

public class WinSequence : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameEvent OnCameraGoToOrigin;

    [Header("UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject historyPrefab;
    [SerializeField] private Transform historyContent;

    private readonly HashSet<byte> redNumbers = new HashSet<byte> {
        1, 3, 5, 7, 9, 12, 14, 16, 18, 19,
        21, 23, 25, 27, 30, 32, 34, 36
    };

    private const int INVALID_RESULT = -1;
    private const int DOUBLE_ZERO = 37;
    private const int MAX_HISTORY = 15;

    public void ShowResult(int result, float totalWin)
    {
        BetPool.Instance.ResetStatus();
        OnCameraGoToOrigin.Invoke();

        string displayResult = GetResultString(result);
        bool isWin = totalWin > 0 && ResultManager.Instance.totalBet > 0;
        bool isRed = redNumbers.Contains((byte)result);

        if (isWin)
        {

            ShowWin(totalWin);
        }
        else
        {
            ShowLose();
        }

        UpdateResultText(displayResult, result, isRed);
        AddToHistory(displayResult, isRed);

        StartCoroutine(EnableBetsAfterDelay());
    }

    private string GetResultString(int result)
    {
        return (result == INVALID_RESULT || result == DOUBLE_ZERO) ? "00" : result.ToString();
    }

    private void ShowWin(float amount)
    {
        winPanel.SetActive(true);
        winText.text = $"WIN <sprite=0> {amount:F2}";
        AudioManager.Instance.PlaySound(0);
    }
    private void ShowLose()
    {
        losePanel.SetActive(true);
        AudioManager.Instance.PlaySound(4);
    }

    private void UpdateResultText(string text, int result, bool isRed)
    {
        resultText.text = text;
        if (text == "0" || text == "00")
            resultText.color = Color.green;
        else
            resultText.color = isRed ? Color.red : Color.white;
    }

    private void AddToHistory(string resultText, bool isRed)
    {
        GameObject historyObj = Instantiate(historyPrefab, historyContent);
        historyObj.transform.SetAsFirstSibling();

        TMP_Text textComponent;
        if (isRed)
        {
            textComponent = historyObj.transform.GetChild(1).GetComponent<TMP_Text>();
            textComponent.text = resultText;
        }
        else
        {
            textComponent = historyObj.transform.GetChild(0).GetComponent<TMP_Text>();
            textComponent.text = resultText;
            textComponent.color = (resultText == "0" || resultText == "00") ? Color.green : Color.black;
        }

        if (historyContent.childCount > MAX_HISTORY)
            Destroy(historyContent.GetChild(MAX_HISTORY).gameObject);
    }

    private IEnumerator EnableBetsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2f);

        BetSpace.EnableBets(true);
        RouletteSceneManager.Instance.GameStarted = false;
        RouletteSceneManager.Instance.UpdateRebetButtonActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
}


