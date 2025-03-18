using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cung cấp cơ chế giật lùi (Knockback) cho nhân vật hoặc kẻ địch khi bị tấn công.
/// </summary>
public class Knockback : MonoBehaviour
{
    // Biến kiểm tra xem đối tượng có đang bị knockback hay không.
    public bool GettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f; // Thời gian đối tượng bị đẩy lùi trước khi dừng lại.

    private Rigidbody2D rb; // Thành phần Rigidbody2D để áp dụng lực đẩy lùi.

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy thành phần Rigidbody2D từ đối tượng.
    }

    /// <summary>
    /// Gây hiệu ứng knockback khi nhận sát thương từ một nguồn nhất định.
    /// </summary>
    /// <param name="damageSource">Vị trí nguồn sát thương.</param>
    /// <param name="knockBackThrust">Lực đẩy lùi.</param>
    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true; // Đánh dấu rằng đối tượng đang bị knockback.

        // Tính toán hướng đẩy lùi (hướng từ nguồn sát thương đến đối tượng).
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;

        // Áp dụng lực đẩy lùi tức thời theo hướng ngược lại.
        rb.AddForce(difference, ForceMode2D.Impulse);

        // Bắt đầu coroutine để dừng knockback sau một khoảng thời gian.
        StartCoroutine(KnockRoutine());
    }

    /// <summary>
    /// Coroutine giúp dừng trạng thái knockback sau một khoảng thời gian nhất định.
    /// </summary>
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);

        // Đặt vận tốc về 0 để đối tượng ngừng di chuyển.
        rb.linearVelocity = Vector2.zero;

        GettingKnockedBack = false; // Đánh dấu rằng đối tượng không còn bị knockback.
    }
}
