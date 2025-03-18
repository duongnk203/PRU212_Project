using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Tốc độ di chuyển của kẻ địch

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Lấy các thành phần cần thiết
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Nếu kẻ địch đang bị đẩy lùi, không di chuyển
        if (knockback.GettingKnockedBack) { return; }

        // Di chuyển kẻ địch theo hướng đã đặt
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        // Lật hướng sprite dựa trên hướng di chuyển
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    /// <summary>
    /// Thiết lập hướng di chuyển cho kẻ địch.
    /// </summary>
    /// <param name="targetPosition">Hướng di chuyển mong muốn.</param>
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    /// <summary>
    /// Dừng di chuyển kẻ địch.
    /// </summary>
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}
