using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cây trượng phép (Staff) là một vũ khí có thể bắn ra tia laser phép thuật.
/// </summary>
public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo; // Thông tin của vũ khí
    [SerializeField] private GameObject magicLaser; // Prefab của tia laser phép thuật
    [SerializeField] private Transform magicLaserSpawnPoint; // Vị trí xuất hiện của laser phép thuật

    private Animator myAnimator;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack"); // Hash giá trị animation "Attack" để tối ưu hiệu suất

    private void Awake()
    {
        myAnimator = GetComponent<Animator>(); // Lấy Animator của cây trượng
    }

    private void Update()
    {
        // Cập nhật hướng của cây trượng theo vị trí chuột
        MouseFollowWithOffset();
    }

    /// <summary>
    /// Kích hoạt đòn tấn công bằng trượng phép.
    /// </summary>
    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH); // Kích hoạt animation tấn công
    }

    /// <summary>
    /// Gọi bởi animation event để tạo ra tia laser phép thuật.
    /// </summary>
    public void SpawnStaffProjectileAnimEvent()
    {
        // Tạo ra một tia laser phép thuật tại vị trí spawn point
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);

        // Cập nhật phạm vi của laser theo thông số vũ khí
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    /// <summary>
    /// Trả về thông tin của vũ khí.
    /// </summary>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    /// <summary>
    /// Điều chỉnh hướng của cây trượng để luôn nhắm về phía con trỏ chuột.
    /// </summary>
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition; // Lấy vị trí chuột trên màn hình
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position); // Lấy vị trí nhân vật trên màn hình

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; // Tính góc xoay theo hướng chuột

        if (mousePos.x < playerScreenPoint.x) // Nếu chuột nằm bên trái nhân vật
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else // Nếu chuột nằm bên phải nhân vật
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
