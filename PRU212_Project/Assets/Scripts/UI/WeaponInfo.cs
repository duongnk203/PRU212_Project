using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lưu trữ thông tin của một loại vũ khí, được sử dụng làm ScriptableObject để dễ quản lý.
/// </summary>
[CreateAssetMenu(menuName = "New Weapon")] // Cho phép tạo mới WeaponInfo từ Unity Editor
public class WeaponInfo : ScriptableObject
{
    [Tooltip("Prefab của vũ khí sẽ được sinh ra khi trang bị.")]
    public GameObject weaponPrefab; // Prefab của vũ khí

    [Tooltip("Thời gian hồi chiêu giữa các lần tấn công.")]
    public float weaponCooldown; // Thời gian hồi chiêu của vũ khí

    [Tooltip("Lượng sát thương mà vũ khí gây ra.")]
    public int weaponDamage; // Lượng sát thương gây ra

    [Tooltip("Tầm tấn công của vũ khí.")]
    public float weaponRange; // Tầm xa của vũ khí
}
