using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

/// <summary>
/// Quản lý hiệu ứng rung màn hình khi có va chạm hoặc sự kiện quan trọng.
/// </summary>
public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    private CinemachineImpulseSource source; // Thành phần tạo rung màn hình của Cinemachine.

    protected override void Awake()
    {
        base.Awake(); // Gọi hàm Awake() của Singleton để đảm bảo chỉ có một instance duy nhất.

        source = GetComponent<CinemachineImpulseSource>(); // Lấy thành phần CinemachineImpulseSource từ GameObject.
    }

    /// <summary>
    /// Gọi hiệu ứng rung màn hình.
    /// </summary>
    public void ShakeScreen()
    {
        source.GenerateImpulse(); // Kích hoạt rung màn hình thông qua Cinemachine.
    }
}
