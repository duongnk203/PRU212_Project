using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Điều khiển hoạt động của cung tên (Bow).
/// Cung có thể bắn tên (Arrow) dựa trên thông tin vũ khí.
/// </summary>
public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo; // Thông tin về vũ khí (tầm bắn, sát thương,...)
    [SerializeField] private GameObject arrowPrefab; // Prefab của mũi tên
    [SerializeField] private Transform arrowSpawnPoint; // Vị trí sinh ra mũi tên khi bắn

    private readonly int FIRE_HASH = Animator.StringToHash("Fire"); // Hash animation "Fire" để tối ưu hiệu suất

    private Animator myAnimator; // Animator của cung để kích hoạt animation bắn

    /// <summary>
    /// Khởi tạo animator khi đối tượng được tạo.
    /// </summary>
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Thực hiện tấn công bằng cung (bắn tên).
    /// Kích hoạt animation bắn và sinh mũi tên tại vị trí quy định.
    /// </summary>
    public void Attack()
    {
        // Gửi tín hiệu kích hoạt animation bắn
        myAnimator.SetTrigger(FIRE_HASH);

        // Tạo mũi tên tại vị trí spawn, quay theo hướng của vũ khí hiện tại
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);

        // Cập nhật phạm vi bắn của mũi tên theo thông tin vũ khí
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
    }

    /// <summary>
    /// Trả về thông tin của cung.
    /// </summary>
    /// <returns>Thông tin vũ khí</returns>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
