using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // Danh sách quái
    public Vector2 topLeft = new Vector2(-13.55f, 8.91f);
    public Vector2 bottomLeft = new Vector2(-13.93f, -8.55f);
    public Vector2 bottomRight = new Vector2(14.74f, -8.42f);
    public Vector2 topRight = new Vector2(14.49f, 6.57f);

    public int goldThreshold1 = 50; // Ngưỡng vàng đầu tiên
    public int goldThreshold2 = 60; // Ngưỡng vàng thứ hai
    public int enemyCountThreshold2 = 8; // Số lượng quái khi đạt 60 vàng

    private int playerGold = 0;
    private bool isRandomSpawnEnabled = false;
    private bool isMassSpawnEnabled = false;

    void Update()
    {
        UpdateGold(EconomyManager.Instance.GetGold());
        CheckGoldThreshold();
    }

    void CheckGoldThreshold()
    {
        if (playerGold >= goldThreshold1 && !isRandomSpawnEnabled)
        {
            isRandomSpawnEnabled = true;
            Debug.Log("Kích hoạt chế độ spawn quái ngẫu nhiên!");
        }

        if (playerGold >= goldThreshold2 && !isMassSpawnEnabled)
        {
            isMassSpawnEnabled = true;
            Debug.Log("Kích hoạt chế độ spawn nhiều quái!");
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0) return;

        int enemyCount = isMassSpawnEnabled ? enemyCountThreshold2 : 1;

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            GameObject enemyPrefab = isRandomSpawnEnabled ? GetRandomEnemyPrefab() : enemyPrefabs[0];
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    GameObject GetRandomEnemyPrefab()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }

    Vector2 GetRandomSpawnPosition()
    {
        float minX = Mathf.Min(topLeft.x, bottomLeft.x);
        float maxX = Mathf.Max(topRight.x, bottomRight.x);
        float minY = Mathf.Min(bottomLeft.y, bottomRight.y);
        float maxY = Mathf.Max(topLeft.y, topRight.y);

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    public void UpdateGold(int amount)
    {
        playerGold = amount;
        Debug.Log("Vàng hiện tại: " + playerGold);
    }
}
