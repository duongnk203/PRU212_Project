using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;  // ❌ Sai namespace, Cinemachine nằm trong UnityEngine

/// <summary>
/// Quản lý camera trong game, sử dụng Cinemachine để theo dõi nhân vật chính.
/// Kế thừa từ Singleton để đảm bảo chỉ có một instance duy nhất.
/// </summary>
public class CameraController : Singleton<CameraController>
{
    // Tham chiếu đến Cinemachine Virtual Camera
    private CinemachineCamera cinemachineVirtualCamera;

    /// <summary>
    /// Khi game bắt đầu, tự động thiết lập camera theo dõi nhân vật.
    /// </summary>
    private void Start()
    {
        SetPlayerCameraFollow();
    }

    /// <summary>
    /// Thiết lập camera để theo dõi nhân vật chính.
    /// </summary>
    public void SetPlayerCameraFollow()
    {
        // Tìm đối tượng Cinemachine Virtual Camera trong scene
        cinemachineVirtualCamera = FindObjectOfType<CinemachineCamera>();

        // Kiểm tra xem có tìm thấy camera hay không để tránh lỗi NullReferenceException
        if (cinemachineVirtualCamera != null && PlayerController.Instance != null)
        {
            cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy CinemachineCamera hoặc PlayerController.");
        }
    }
}
