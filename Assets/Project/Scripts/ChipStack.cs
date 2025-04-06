using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipStack : MonoBehaviour {

    public static readonly int[] CHIP_VALUES = new int[] { 1, 5, 10, 25, 50, 100 };
    public static readonly Vector3 CollectPosition = new Vector3(0,0,-3);

    private Vector3 initialPosition;
    private float value = 0;

    private List<GameObject> chips;
      
    private const float MoveDuration = 1.0f;  // Movement duration for chip collection
    private const float CollectDelay = 1.5f; // Delay before starting chip collection animation
    private bool isMoving = false;  // To prevent multiple moves at once


    void Start()
    {
        initialPosition = transform.position;
    }

    public void SetInitialPosition(Vector3 pos)
    {
        transform.position = pos;
        initialPosition = pos;
    }

    public void Add(float value)
    {
        SetValue(this.value + value);
    }

    public void Remove(float value)
    {
        SetValue(this.value - value);
    }

    public float Clear()
    {
        float lastBet = value;
        value = 0;
        transform.position = initialPosition;

        if (chips != null)
        {
            foreach (GameObject chip in chips)
            {
                Destroy(chip);
            }
        }
        chips = null;
        return lastBet;
    }

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float value)
    {
        Clear();

        if (value <= 0)
        {
            return;
        }

        this.value = value;
         chips = new List<GameObject>();

        int currentChipIndex = CHIP_VALUES.Length - 1;

        while (value > 0)
        {
            float nextValue = value - CHIP_VALUES[currentChipIndex];

            if (nextValue < 0)
            {
                currentChipIndex--;
                if (currentChipIndex < 0)
                {
                    throw new Exception("Impossible value");
                }
                continue;
            }

            value = nextValue;

            GameObject newChip = ChipManager.Instance.InstantiateChip(currentChipIndex);
            newChip.transform.parent = gameObject.transform;
            newChip.transform.localPosition = new Vector3(0, .01f * (chips.Count + 1), 0);

            chips.Add(newChip);
        }
    }

    public float Win(int multiplier)
    {
        float winAmount = value * multiplier;
        SetValue(winAmount);

        if (winAmount > 0)
        {
            CollectChips();
        }

        return winAmount;
    }

  
    public void CollectChips()
    {
        if (isMoving)
            return;

        StartCoroutine(MoveChipsToCollectPosition());
    }
    private IEnumerator MoveChipsToCollectPosition()
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        // Move the chips to the collect position over the duration
        while (elapsedTime < MoveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, CollectPosition, elapsedTime / MoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it ends exactly at the target position
        transform.position = CollectPosition;

        // Clear chips after the delay
        yield return new WaitForSeconds(CollectDelay);
        Clear();

        isMoving = false;
    }
}
