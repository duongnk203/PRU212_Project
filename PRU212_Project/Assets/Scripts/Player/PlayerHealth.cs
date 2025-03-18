using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Quản lý hệ thống máu của người chơi, bao gồm hồi máu, nhận sát thương và chết.
/// </summary>
public class PlayerHealth : Singleton<PlayerHealth>
{
    /// <summary>
    /// Kiểm tra xem người chơi đã chết chưa.
    /// </summary>
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 3; // Máu tối đa của người chơi
    [SerializeField] private float knockBackThrustAmount = 10f; // Lực đẩy khi bị đánh
    [SerializeField] private float damageRecoveryTime = 1f; // Thời gian hồi phục sau khi nhận sát thương

    private Slider healthSlider; // Thanh máu của người chơi
    private int currentHealth; // Máu hiện tại
    private bool canTakeDamage = true; // Kiểm tra xem người chơi có thể nhận sát thương hay không
    private Knockback knockback; // Thành phần xử lý knockback
    private Flash flash; // Hiệu ứng nhấp nháy khi trúng đòn

    // Các hằng số để lấy thông tin từ UI và cảnh
    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Scene1";
    readonly int DEATH_HASH = Animator.StringToHash("Death"); // Hash animation chết

    /// <summary>
    /// Khởi tạo các thành phần cần thiết.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;

        UpdateHealthSlider();
    }

    /// <summary>
    /// Xử lý khi người chơi va chạm với kẻ địch.
    /// </summary>
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    /// <summary>
    /// Hồi một đơn vị máu cho người chơi.
    /// </summary>
    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    /// <summary>
    /// Xử lý khi người chơi nhận sát thương.
    /// </summary>
    /// <param name="damageAmount">Lượng sát thương nhận vào</param>
    /// <param name="hitTransform">Vị trí của kẻ tấn công</param>
    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) return;

        ScreenShakeManager.Instance.ShakeScreen(); // Rung màn hình khi bị đánh
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount); // Đẩy lùi người chơi
        StartCoroutine(flash.FlashRoutine()); // Hiệu ứng nhấp nháy
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine()); // Hồi phục sau khi trúng đòn
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    /// <summary>
    /// Kiểm tra xem người chơi có chết hay không.
    /// </summary>
    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject); // Hủy vũ khí đang sử dụng
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH); // Kích hoạt animation chết
            StartCoroutine(DeathLoadSceneRoutine()); // Chuyển cảnh khi chết
        }
    }

    /// <summary>
    /// Đợi một khoảng thời gian trước khi chuyển cảnh sau khi chết.
    /// </summary>
    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }

    /// <summary>
    /// Thời gian hồi phục sau khi nhận sát thương.
    /// </summary>
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    /// <summary>
    /// Cập nhật thanh máu trên UI.
    /// </summary>
    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
