using UnityEngine;

[CreateAssetMenu(fileName = "TestData", menuName = "TestData", order = 0)]
public class TestData : ScriptableObject
{
    [SerializeField] private bool isTest;
    [SerializeField] private int winNumber;

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
}
