using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    private void Start()
    {
        goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        UpdateGoldUI();
    }

    public void UpdateCurrentGold()
    {
        currentGold += 1;
        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        goldText.text = currentGold.ToString("D3");
    }

    public void ResetGold()
    {
        currentGold = 0;
        UpdateGoldUI(); 
    }

    public int GetGold()
    {
        return currentGold;
    }
    public void SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("Không đủ vàng để thực hiện giao dịch!");
        }
    }
}
