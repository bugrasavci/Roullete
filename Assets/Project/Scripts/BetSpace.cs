using System.Collections;
using UnityEngine;

[System.Serializable]
public enum BetType
{
    Straight,
    Split,
    Corner,
    Street,
    DoubleStreet,
    Row,
    Dozen,
    Low,
    High,
    Even,
    Odd,
    Red,
    Black
}

public class BetSpace : MonoBehaviour
{
    public ChipStack stack;
    public BetType betType;
    public static int numLenght = 37;

    [SerializeField]
    public int[] winningNumbers;

    public MeshRenderer[] betSpaceRender;

    private MeshRenderer mesh;
    private float lastBet = 0;

    public static bool BetsEnabled { get; private set; } = true;

    public float GetValue() => stack.GetValue();

    void Start()
    {
        InitializeMesh();
        InitializeStack();
        RegisterSelf();
    }

    private void InitializeMesh()
    {
        mesh = GetComponent<MeshRenderer>();
        if (mesh) mesh.enabled = false;
    }

    private void InitializeStack()
    {
        stack = Cloth.Instance.InstanceStack();
        stack.SetInitialPosition(transform.position);
        stack.transform.SetParent(transform);
        stack.transform.localPosition = Vector3.zero;
    }

    private void RegisterSelf()
    {
        ResultManager.Instance.RegisterBetSpace(this);
    }

    private void OnMouseEnter()
    {
        ToolTipManager.Instance.SelectTarget(stack);

        if (BetsEnabled)
        {
            SetHighlight(true);
        }
    }

    void OnMouseExit()
    {
        ToolTipManager.Instance.Deselect();


        if (BetsEnabled)
        {
            SetHighlight(false);
        }
    }
    private void SetHighlight(bool enable)
    {
        if (mesh) mesh.enabled = enable;

        foreach (var renderer in betSpaceRender)
        {
            renderer.enabled = enable;
        }
    }

    private void OnMouseUp()
    {
        float selectedValue = ChipManager.Instance.GetSelectedValue();
        ApplyBet(selectedValue);
        ToolTipManager.Instance.SelectTarget(stack);
    }

    public void ApplyBet(float selectedValue)
    {
        if (!BetLimitManager.Instance.AllowLimit(selectedValue))
            return;

        if (BetsEnabled && selectedValue > 0 && BalanceManager.Instance.Balance - selectedValue >= 0)
        {
            AudioManager.Instance.PlaySound(3);

            BalanceManager.Instance.ChangeBalance(-selectedValue);
            ResultManager.Instance.totalBet += selectedValue;
            stack.Add(selectedValue);

            lastBet = stack.GetValue();

            BetPool.Instance.AddBet(this, selectedValue);

            RouletteSceneManager.Instance.UpdateUIInteractable(true);
            RouletteSceneManager.Instance.UpdateRebetButtonActive(false);
            RouletteSceneManager.Instance.UpdateLocalPlayerText();
        }
    }

    public void RemoveBet(float value)
    {
        BalanceManager.Instance.ChangeBalance(value);
        ResultManager.Instance.totalBet -= value;
        stack.Remove(value);
        lastBet = stack.GetValue();
        RouletteSceneManager.Instance.UpdateLocalPlayerText();
    }

    public float ResolveBet(int result)
    {
        int multiplier = numLenght / winningNumbers.Length;

        bool won = false;

        foreach (int num in winningNumbers)
        {
            if (num == result)
            {
                won = true;

                if (mesh && betType == BetType.Straight)
                    StartCoroutine(FlashMesh());
                break;
            }
        }

        float winAmount = 0;

        if (won)
        {
            winAmount = stack.Win(multiplier);
        }
        else
        {
            stack.Clear();
        }

        return winAmount;
    }
    private IEnumerator FlashMesh()
    {
        mesh.enabled = true;
        yield return new WaitForSeconds(2f);
        mesh.enabled = false;
    }

    public void Rebet()
    {
        if (lastBet == 0)
            return;

        if (!BetLimitManager.Instance.AllowLimit(lastBet))
        {
            lastBet = 0;
            return;
        }

        if (BetsEnabled && BalanceManager.Instance.Balance - lastBet >= 0)
        {
            BalanceManager.Instance.ChangeBalance(-lastBet);
            ResultManager.Instance.totalBet += lastBet;
            stack.SetValue(lastBet);
            lastBet = stack.GetValue();

            BetPool.Instance.AddBet(this, lastBet);

            RouletteSceneManager.Instance.UpdateUIInteractable(true);
            RouletteSceneManager.Instance.UpdateRebetButtonActive(false);
            RouletteSceneManager.Instance.UpdateLocalPlayerText();
        }
        else
            lastBet = 0;
    }

    public void Clear()
    {
        float val = stack.GetValue();
        BalanceManager.Instance.ChangeBalance(val);
        ResultManager.Instance.totalBet -= val;
        lastBet = 0;

        stack.Clear();
        RouletteSceneManager.Instance.UpdateLocalPlayerText();
    }

    public static void EnableBets(bool enable)
    {
        BetsEnabled = enable;
    }
}