using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Thực hiện hiệu ứng mờ dần (fade out) cho SpriteRenderer trước khi hủy đối tượng.
/// </summary>
public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float fadeTime = .4f; // Thời gian để hoàn tất hiệu ứng mờ dần

    private SpriteRenderer spriteRenderer; // Tham chiếu đến SpriteRenderer của đối tượng

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy component SpriteRenderer
    }

    /// <summary>
    /// Coroutine thực hiện hiệu ứng mờ dần của SpriteRenderer theo thời gian.
    /// </summary>
    public IEnumerator SlowFadeRoutine()
    {
        float elapsedTime = 0f; // Thời gian đã trôi qua kể từ khi bắt đầu fade
        float startValue = spriteRenderer.color.a; // Lấy giá trị alpha ban đầu của sprite

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime; // Tăng thời gian đã trôi qua
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime); // Tính toán alpha mới

            // Cập nhật màu sắc của sprite với giá trị alpha mới
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

            yield return null; // Đợi đến frame tiếp theo
        }

        Destroy(gameObject); // Hủy đối tượng sau khi hoàn tất hiệu ứng mờ dần
    }
}
