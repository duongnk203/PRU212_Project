using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Xác định nguồn gây sát thương và truyền sát thương lên mục tiêu bị va chạm.
/// </summary>
public class DamageSource : MonoBehaviour
{
    private int damageAmount; // Lượng sát thương gây ra.

    private void Start()
    {
        // Lấy vũ khí hiện tại từ hệ thống ActiveWeapon.
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;

        // Chuyển đổi vũ khí sang interface IWeapon để lấy thông tin sát thương.
        damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có phải là kẻ địch hay không.
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

        // Nếu có, gây sát thương lên kẻ địch.
        enemyHealth?.TakeDamage(damageAmount);
    }
}
