using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Đại diện cho một ô (slot) trong kho đồ.
/// Mỗi ô có thể chứa một vũ khí cụ thể.
/// </summary>
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo; // Thông tin vũ khí được lưu trong ô này

    /// <summary>
    /// Lấy thông tin vũ khí từ ô kho đồ.
    /// </summary>
    /// <returns>WeaponInfo của vũ khí đang chứa trong slot</returns>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
