using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class này chịu trách nhiệm spawn (tạo) các vật phẩm khi một đối tượng bị tiêu diệt.
/// </summary>
public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin;   // Prefab của đồng xu vàng
    [SerializeField] private GameObject healthGlobe; // Prefab của vật phẩm hồi máu
    [SerializeField] private GameObject staminaGlobe; // Prefab của vật phẩm hồi thể lực

    /// <summary>
    /// Hàm này sẽ quyết định vật phẩm nào được spawn một cách ngẫu nhiên.
    /// </summary>
    public void DropItems()
    {
        int randomNum = Random.Range(1, 5); // Chọn số ngẫu nhiên từ 1 đến 4

        if (randomNum == 1)
        {
            // Spawn vật phẩm hồi máu
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            // Spawn vật phẩm hồi thể lực
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            // Spawn từ 1 đến 3 đồng xu vàng
            int randomAmountOfGold = Random.Range(1, 4);

            for (int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            }
        }
    }
}
