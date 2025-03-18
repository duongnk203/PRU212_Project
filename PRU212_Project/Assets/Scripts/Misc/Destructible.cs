using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Đối tượng có thể bị phá hủy khi va chạm với các vật thể gây sát thương (DamageSource hoặc Projectile).
/// </summary>
public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX; // Hiệu ứng phá hủy khi vật thể bị vỡ

    /// <summary>
    /// Xử lý va chạm khi có một đối tượng khác đi vào collider của vật thể này.
    /// </summary>
    /// <param name="other">Đối tượng va chạm</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm là một nguồn gây sát thương (DamageSource) hoặc một viên đạn (Projectile)
        if (other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>())
        {
            // Gọi phương thức để rơi vật phẩm (nếu có)
            GetComponent<PickUpSpawner>().DropItems();

            // Tạo hiệu ứng phá hủy tại vị trí của đối tượng
            Instantiate(destroyVFX, transform.position, Quaternion.identity);

            // Hủy vật thể này khỏi cảnh
            Destroy(gameObject);
        }
    }
}
