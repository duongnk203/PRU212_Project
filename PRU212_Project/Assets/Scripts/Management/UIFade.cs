using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Quản lý hiệu ứng chuyển cảnh bằng cách làm mờ màn hình (Fade In/Out).
/// Kế thừa từ Singleton để đảm bảo chỉ có một thể hiện duy nhất.
/// </summary>
public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image fadeScreen; // Ảnh overlay để làm hiệu ứng mờ màn hình
    [SerializeField] private float fadeSpeed = 1f; // Tốc độ mờ màn hình

    private IEnumerator fadeRoutine; // Tham chiếu đến Coroutine hiện tại để có thể dừng khi cần thiết

    /// <summary>
    /// Làm mờ màn hình về màu đen (Fade Out).
    /// </summary>
    public void FadeToBlack()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine); // Dừng hiệu ứng hiện tại (nếu có) để tránh xung đột
        }

        fadeRoutine = FadeRoutine(1); // Mục tiêu alpha = 1 (đen hoàn toàn)
        StartCoroutine(fadeRoutine);
    }

    /// <summary>
    /// Làm màn hình trở lại trong suốt (Fade In).
    /// </summary>
    public void FadeToClear()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(0); // Mục tiêu alpha = 0 (trong suốt)
        StartCoroutine(fadeRoutine);
    }

    /// <summary>
    /// Coroutine thực hiện hiệu ứng mờ màn hình.
    /// </summary>
    /// <param name="targetAlpha">Giá trị alpha mục tiêu (0 = trong suốt, 1 = đen hoàn toàn)</param>
    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha)) // Kiểm tra nếu giá trị alpha chưa đạt mục tiêu
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}
