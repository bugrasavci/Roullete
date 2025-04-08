using UnityEngine;

[CreateAssetMenu(fileName = "TestData", menuName = "TestData", order = 0)]
public class TestData : ScriptableObject
{
    [SerializeField] private bool isTest;
    [SerializeField] private int winNumber;
    [SerializeField] private float maxBet = 1000f;
    [SerializeField] private float minBet = 1f;

    public bool IsTest
    {
        get => isTest;
        set => isTest = value;
    }
    public int WinNumber
    {
        get => winNumber;
        set => winNumber = value;
    }
    public float MaxBet
    {
        get => maxBet;
        set => maxBet = value;
    }
    public float MinBet
    {
        get => minBet;
        set => minBet = value;
    }
}
