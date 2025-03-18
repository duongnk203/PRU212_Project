using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Thêm thư viện SceneManager

public class EnemyManager : MonoBehaviour
{
    public List<EnemyPathfinding> enemies = new List<EnemyPathfinding>(); // Danh sách kẻ địch
    public GameObject blockObject; // Object chặn nhân vật

    private string currentScene; // Lưu tên scene hiện tại

    private void OnEnable()
    {
        // Đăng ký sự kiện Enemy chết
        EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi object bị hủy
        EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;
    }

    void Start()
    {
        // Lấy tên scene hiện tại
        currentScene = SceneManager.GetActiveScene().name;
        UpdateEnemyList(); // Cập nhật danh sách enemy ban đầu
    }

    void Update()
    {
        CleanEnemyList(); // Loại bỏ các enemy đã bị Destroy

        if (enemies.Count == 0 && blockObject != null)
        {
            Destroy(blockObject); // Nếu không còn enemy, hủy object chặn
        }
    }

    /// <summary>
    /// Xử lý khi một enemy bị tiêu diệt (được gọi từ EnemyHealth).
    /// </summary>
    private void HandleEnemyDeath()
    {
        CleanEnemyList(); // Cập nhật danh sách enemy

        if (currentScene == "Scene1")
        {
            IncreaseSpeedForRemainingEnemies(1.0f);
        }
        else if (currentScene == "Scene2")
        {
            IncreaseSpeedAndHealthForRemainingEnemies(1.0f, 1);
        }
    }

    /// <summary>
    /// Tăng tốc độ của tất cả các enemy còn lại.
    /// </summary>
    private void IncreaseSpeedForRemainingEnemies(float amount)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.IncreaseSpeed(amount);
            }
        }
    }

    /// <summary>
    /// Tăng tốc độ và máu của tất cả enemy còn lại.
    /// </summary>
    private void IncreaseSpeedAndHealthForRemainingEnemies(float speedAmount, int healthAmount)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.IncreaseSpeed(speedAmount);

                // Tăng máu cho enemy
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.IncreaseHealth(healthAmount);
                }
            }
        }
    }

    /// <summary>
    /// Cập nhật danh sách enemy bằng cách tìm tất cả enemy trong scene.
    /// </summary>
    void UpdateEnemyList()
    {
        enemies.Clear();
        foreach (var enemy in FindObjectsOfType<EnemyPathfinding>())
        {
            enemies.Add(enemy);
        }
    }

    /// <summary>
    /// Xóa các phần tử null khỏi danh sách enemy.
    /// </summary>
    void CleanEnemyList()
    {
        enemies.RemoveAll(e => e == null);
    }
}
