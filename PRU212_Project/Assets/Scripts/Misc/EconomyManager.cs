using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Quản lý hệ thống tiền tệ trong game.
/// </summary>
public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText; // Đối tượng UI hiển thị số lượng vàng
    private int currentGold = 0; // Số vàng hiện tại của người chơi

    const string COIN_AMOUNT_TEXT = "Gold Amount Text"; // Tên đối tượng UI hiển thị vàng

    /// <summary>
    /// Cập nhật số vàng hiện tại và hiển thị trên UI.
    /// </summary>
    public void UpdateCurrentGold()
    {
        currentGold += 1; // Tăng số vàng lên 1 đơn vị

        // Kiểm tra nếu goldText chưa được tham chiếu, thì tìm và gán đối tượng UI tương ứng
        if (goldText == null)
        {
            GameObject goldTextObject = GameObject.Find(COIN_AMOUNT_TEXT);
            if (goldTextObject != null)
            {
                goldText = goldTextObject.GetComponent<TMP_Text>();
            }
        }

        // Nếu tìm thấy goldText, cập nhật nội dung hiển thị số vàng
        if (goldText != null)
        {
            goldText.text = currentGold.ToString("D3"); // Hiển thị số vàng với 3 chữ số (ví dụ: 001, 012, 120)
        }
    }
}
