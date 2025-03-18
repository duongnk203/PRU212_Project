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

    // ✅ Hàm mới: Reset Gold khi restart game
    public void ResetGold()
    {
        currentGold = 0; // Đặt lại Gold về 0
        UpdateGoldUI();  // Cập nhật UI ngay lập tức
    }
}
