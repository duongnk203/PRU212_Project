using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return; // Tránh lỗi khi object bị tắt

        if (other.gameObject.GetComponent<PlayerController>())
        {
            StartFade(spriteRenderer, fadeTime, transparencyAmount);
            StartFade(tilemap, fadeTime, transparencyAmount);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return; // Tránh lỗi khi object bị tắt

        if (other.gameObject.GetComponent<PlayerController>())
        {
            StartFade(spriteRenderer, fadeTime, 1f);
            StartFade(tilemap, fadeTime, 1f);
        }
    }

    private void StartFade(SpriteRenderer targetRenderer, float fadeTime, float targetAlpha)
    {
        if (targetRenderer)
        {
            StartCoroutine(FadeRoutine(targetRenderer, fadeTime, targetRenderer.color.a, targetAlpha));
        }
    }

    private void StartFade(Tilemap targetTilemap, float fadeTime, float targetAlpha)
    {
        if (targetTilemap)
        {
            StartCoroutine(FadeRoutine(targetTilemap, fadeTime, targetTilemap.color.a, targetAlpha));
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer targetRenderer, float fadeTime, float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
            targetRenderer.color = new Color(targetRenderer.color.r, targetRenderer.color.g, targetRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap targetTilemap, float fadeTime, float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
            targetTilemap.color = new Color(targetTilemap.color.r, targetTilemap.color.g, targetTilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
