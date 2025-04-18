﻿using System;
using Assets.Project.Scripts;
using Assets.Project.Scripts.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteSceneManager : Singleton<RouletteSceneManager>
{    
    [Header("Events")]
    [SerializeField] private GameEvent onBetPoolClearAll;
    [SerializeField] private GameEvent onBetPoolUndo;
    [SerializeField] private GameEvent onBetPoolRebet;
    [SerializeField] private GameEvent onWheelSpin;
    [SerializeField] private GameEvent OnCameraGoToTarget;

    [Header("State")]
    public bool GameStarted = false;
    public bool IsPopupOpen = false;
    
    [Header("Text UI")]
    [SerializeField] private TMP_Text textBalance;
    [SerializeField] private TMP_Text textBet;
    [SerializeField] private TMP_Text resultText;
       
    [Header("Buttons")]
    [SerializeField] private Button clearButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private Button rebetButton;
    [SerializeField] private Button rollButton;

    [SerializeField] BalanceSavedData balanceSavedData;
    [SerializeField] GameObject SelectWinNumberPanel;
    [SerializeField] TMP_InputField SelectWinNumberInputField;
    [SerializeField] TestData testData;


    public void OnButtonClear()
    {
        PlayButtonSound();
        UpdateUIInteractable(false);
        onBetPoolClearAll.Invoke();
    }

    public void OnButtonUndo()
    {
        PlayButtonSound();
        undoButton.interactable = false;
        onBetPoolUndo.Invoke();
    }

    public void OnButtonRebet()
    {
        PlayButtonSound();
        rebetButton.gameObject.SetActive(false);
        onBetPoolRebet.Invoke();
    }

    public void OnButtonRoll()
    {
        testData.IsTest = false;
        testData.WinNumber = -1;
        SelectWinNumberPanel.SetActive(true);
        IsPopupOpen = true;
    }
    public void OnButtonContinue()
    {
        if (!String.IsNullOrWhiteSpace(SelectWinNumberInputField.text))
        {
          
            testData.IsTest = true;
            testData.WinNumber = int.Parse(SelectWinNumberInputField.text);
        }
        SelectWinNumberPanel.SetActive(false);
        IsPopupOpen = false;
        SelectWinNumberInputField.text = "";
        UpdateUIInteractable(false);
        resultText.text = "";
        SpinRoulette();
    }



    private void SpinRoulette()
    {
        onWheelSpin.Invoke();
        AudioManager.Instance.PlaySound(2);
        ChangeUIOnSpin();
    }

    private void ChangeUIOnSpin()
    {
        OnCameraGoToTarget.Invoke();
        ToolTipManager.Instance.Deselect();
        UpdateUIInteractable(false);
        rebetButton.gameObject.SetActive(false);
        ChipManager.Instance.EnableChips(false);
    }

    public void UpdateLocalPlayerText()
    {
        textBet.text = $"Bet: <sprite=0> {ResultManager.Instance.totalBet.ToString("F2")}";
        textBalance.text = $"<sprite=0> {balanceSavedData.Balance.ToString("F2")}";
    }

    public void UpdateUIInteractable(bool state)
    {
        clearButton.interactable = state;
        undoButton.interactable = state;
        rollButton.interactable = state;
    }
    public void UpdateRebetButtonActive(bool state)
    {
        rebetButton.gameObject.SetActive(state);
    }

    private void PlayButtonSound()
    {
        AudioManager.Instance?.PlaySound(3);
    }
}

