using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFade spriteFade;

    private void Awake()
    {
        // Lấy thành phần SpriteFade để xử lý hiệu ứng mờ dần
        spriteFade = GetComponent<SpriteFade>();
    }

    private void Start()
    {
        // Bắt đầu hiệu ứng mờ dần chậm
        StartCoroutine(spriteFade.SlowFadeRoutine());

        // Vô hiệu hóa collider sau 0.2 giây
        Invoke("DisableCollider", 0.2f);
    }

    /// <summary>
    /// Gây sát thương cho người chơi khi va chạm.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }

    /// <summary>
    /// Vô hiệu hóa Collider của đối tượng.
    /// </summary>
    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
