using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý vũ khí hiện tại của người chơi và xử lý tấn công.
/// </summary>
public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; } // Vũ khí hiện tại đang được trang bị.

    private PlayerControls playerControls; // Điều khiển của người chơi.
    private float timeBetweenAttacks; // Khoảng thời gian giữa các đòn tấn công.

    private bool attackButtonDown, isAttacking = false; // Trạng thái tấn công.

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls(); // Khởi tạo hệ thống điều khiển.
    }

    private void OnEnable()
    {
        playerControls.Enable(); // Kích hoạt điều khiển khi GameObject được bật.
    }

    private void Start()
    {
        // Đăng ký sự kiện bấm và nhả nút tấn công.
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown(); // Khởi tạo cooldown để tránh spam đòn tấn công ngay khi game bắt đầu.
    }

    private void Update()
    {
        Attack();
    }

    /// <summary>
    /// Trang bị vũ khí mới cho người chơi.
    /// </summary>
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;

        AttackCooldown(); // Đặt cooldown ban đầu cho vũ khí mới.
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown; // Lấy thời gian cooldown từ vũ khí.
    }

    /// <summary>
    /// Gỡ bỏ vũ khí hiện tại.
    /// </summary>
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    /// <summary>
    /// Kích hoạt cooldown sau khi tấn công.
    /// </summary>
    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    /// <summary>
    /// Coroutine kiểm soát thời gian giữa các đòn tấn công.
    /// </summary>
    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    /// <summary>
    /// Gọi khi nút tấn công được nhấn.
    /// </summary>
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    /// <summary>
    /// Gọi khi nút tấn công được thả.
    /// </summary>
    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    /// <summary>
    /// Xử lý hành động tấn công.
    /// </summary>
    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown(); // Kích hoạt cooldown sau khi tấn công.
            (CurrentActiveWeapon as IWeapon).Attack(); // Gọi hành động tấn công từ vũ khí.
        }
    }
}
