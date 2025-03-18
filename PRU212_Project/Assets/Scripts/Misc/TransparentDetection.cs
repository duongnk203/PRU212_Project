using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Xử lý hiệu ứng trong suốt cho các đối tượng SpriteRenderer và Tilemap khi người chơi đi qua.
/// </summary>
public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f; // Mức độ trong suốt khi người chơi đi qua.
    [SerializeField] private float fadeTime = .4f; // Thời gian để hiệu ứng mờ dần diễn ra.

    private SpriteRenderer spriteRenderer; // Thành phần hiển thị sprite (nếu có).
    private Tilemap tilemap; // Thành phần hiển thị Tilemap (nếu có).

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Kiểm tra xem có SpriteRenderer không.
        tilemap = GetComponent<Tilemap>(); // Kiểm tra xem có Tilemap không.
    }

    /// <summary>
    /// Khi người chơi bước vào vùng trigger, làm đối tượng mờ đi.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) // Kiểm tra nếu đối tượng va chạm là người chơi.
        {
            if (spriteRenderer) // Nếu có SpriteRenderer, làm mờ nó.
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            }
            else if (tilemap) // Nếu có Tilemap, làm mờ nó.
            {
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

    /// <summary>
    /// Khi người chơi rời khỏi vùng trigger, khôi phục độ trong suốt về bình thường.
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) // Kiểm tra nếu đối tượng rời đi là người chơi.
        {
            if (spriteRenderer) // Nếu có SpriteRenderer, khôi phục độ trong suốt.
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
            }
            else if (tilemap) // Nếu có Tilemap, khôi phục độ trong suốt.
            {
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }

    /// <summary>
    /// Coroutine để làm mờ hoặc khôi phục độ trong suốt của SpriteRenderer.
    /// </summary>
    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine để làm mờ hoặc khôi phục độ trong suốt của Tilemap.
    /// </summary>
    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
