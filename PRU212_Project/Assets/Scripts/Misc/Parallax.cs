using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hiệu ứng Parallax cho nền (background) di chuyển theo camera với tốc độ khác nhau.
/// </summary>
public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f; // Hệ số dịch chuyển của Parallax, giá trị âm để tạo hiệu ứng nền di chuyển chậm hơn camera.

    private Camera cam; // Camera chính trong game.
    private Vector2 startPos; // Vị trí ban đầu của nền.

    // Tính toán quãng đường mà camera đã di chuyển kể từ khi bắt đầu.
    private Vector2 travel => (Vector2)cam.transform.position - startPos;

    private void Awake()
    {
        cam = Camera.main; // Lấy Camera chính.
    }

    private void Start()
    {
        startPos = transform.position; // Lưu lại vị trí ban đầu của đối tượng.
    }

    private void FixedUpdate()
    {
        // Cập nhật vị trí của đối tượng dựa trên sự di chuyển của camera, tạo hiệu ứng parallax.
        transform.position = startPos + travel * parallaxOffset;
    }
}
