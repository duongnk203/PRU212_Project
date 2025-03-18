using System.Collections;
using UnityEngine;
using System; // Thêm thư viện để sử dụng Event

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int startingHealth = 3; // Máu ban đầu

    [Header("Effects")]
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f; // Lực đẩy lùi khi bị đánh

    private int currentHealth; // Máu hiện tại
    private Knockback knockback;
    private Flash flash;

    // Event cho các hệ thống khác có thể đăng ký khi kẻ địch chết
    public static event Action OnEnemyDeath;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Gây sát thương lên kẻ địch
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0); // Đảm bảo máu không xuống dưới 0
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust); // Gây knockback
        StartCoroutine(flash.FlashRoutine()); // Hiệu ứng nháy trắng
        DetectDeath(); // Kiểm tra xem kẻ địch có chết không
    }

    /// <summary>
    /// Kiểm tra nếu máu <= 0 thì hủy kẻ địch
    /// </summary>
    private void DetectDeath()
    {
        if (currentHealth > 0) return; // Nếu còn máu, không làm gì cả

        // Kích hoạt hiệu ứng chết nếu có
        if (deathVFXPrefab != null)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        }

        // Gọi sự kiện khi kẻ địch chết
        OnEnemyDeath?.Invoke();

        // Drop items nếu có PickUpSpawner
        PickUpSpawner pickUpSpawner = GetComponent<PickUpSpawner>();
        if (pickUpSpawner != null)
        {
            pickUpSpawner.DropItems();
        }

        Destroy(gameObject);
    }
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;  // Tăng máu hiện tại
        startingHealth += amount; // Cập nhật máu tối đa để enemy có thể hồi phục trong tương lai
    }

}
