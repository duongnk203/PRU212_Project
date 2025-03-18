using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Điều khiển hiệu ứng chém (slash animation) bằng Particle System.
/// Khi hiệu ứng kết thúc, đối tượng sẽ tự hủy.
/// </summary>
public class SlashAnim : MonoBehaviour
{
    private ParticleSystem ps; // Hệ thống hạt (particle system) cho hiệu ứng chém

    private void Awake()
    {
        // Lấy thành phần ParticleSystem từ GameObject
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Kiểm tra nếu particle system đã kết thúc (không còn hoạt động)
        if (ps && !ps.IsAlive())
        {
            DestroySelf();
        }
    }

    /// <summary>
    /// Hủy đối tượng khi hiệu ứng kết thúc.
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
