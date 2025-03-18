using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hiệu ứng nhấp nháy trắng khi nhân vật hoặc vật thể bị tấn công.
/// </summary>
public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat; // Material trắng để tạo hiệu ứng nhấp nháy
    [SerializeField] private float restoreDefaultMatTime = .2f; // Thời gian hiệu ứng trước khi khôi phục Material gốc

    private Material defaultMat; // Material mặc định của đối tượng
    private SpriteRenderer spriteRenderer; // Thành phần SpriteRenderer của đối tượng

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer từ đối tượng
        defaultMat = spriteRenderer.material; // Lưu lại Material mặc định
    }

    /// <summary>
    /// Trả về thời gian khôi phục Material mặc định.
    /// </summary>
    public float GetRestoreMatTime()
    {
        return restoreDefaultMatTime;
    }

    /// <summary>
    /// Coroutine thực hiện hiệu ứng nhấp nháy.
    /// Chuyển sang Material trắng trong một khoảng thời gian rồi trở về Material mặc định.
    /// </summary>
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat; // Chuyển sang Material trắng
        yield return new WaitForSeconds(restoreDefaultMatTime); // Đợi một khoảng thời gian
        spriteRenderer.material = defaultMat; // Khôi phục Material gốc
    }
}
