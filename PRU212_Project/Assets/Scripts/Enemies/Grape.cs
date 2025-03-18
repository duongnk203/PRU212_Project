using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab; // Dựng sẵn đạn của kẻ địch "Grape"

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack"); // Hash animation "Attack" để tối ưu hiệu suất

    private void Awake()
    {
        // Lấy các thành phần cần thiết
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Kích hoạt hành động tấn công của kẻ địch.
    /// </summary>
    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH); // Kích hoạt animation tấn công

        // Kiểm tra vị trí của người chơi để xoay hướng của kẻ địch
        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            spriteRenderer.flipX = false; // Không lật khi người chơi ở bên phải
        }
        else
        {
            spriteRenderer.flipX = true; // Lật khi người chơi ở bên trái
        }
    }

    /// <summary>
    /// Sự kiện trong animation: Sinh ra đạn của kẻ địch "Grape".
    /// </summary>
    public void SpawnProjectileAnimEvent()
    {
        Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
