using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý hệ thống trang bị vũ khí từ kho đồ (Inventory).
/// Sử dụng Singleton để đảm bảo chỉ có một thể hiện duy nhất.
/// </summary>
public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0; // Chỉ số của slot đang được kích hoạt

    private PlayerControls playerControls; // Điều khiển bàn phím của người chơi

    /// <summary>
    /// Khởi tạo Singleton và hệ thống điều khiển.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    /// <summary>
    /// Đăng ký sự kiện thay đổi vũ khí khi người chơi nhấn phím số.
    /// </summary>
    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    /// <summary>
    /// Bật điều khiển khi GameObject được kích hoạt.
    /// </summary>
    private void OnEnable()
    {
        playerControls.Enable();
    }

    /// <summary>
    /// Trang bị vũ khí ban đầu khi bắt đầu trò chơi.
    /// </summary>
    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    /// <summary>
    /// Chuyển đổi slot vũ khí khi người chơi nhấn số trên bàn phím.
    /// </summary>
    /// <param name="numValue">Số slot vũ khí được chọn.</param>
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1); // Chuyển đổi vũ khí dựa vào chỉ số slot
    }

    /// <summary>
    /// Hiển thị hiệu ứng chọn slot vũ khí và thay đổi vũ khí.
    /// </summary>
    /// <param name="indexNum">Chỉ số của slot cần kích hoạt.</param>
    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        // Ẩn highlight của tất cả slot vũ khí trong kho đồ
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        // Hiển thị highlight cho slot đang được chọn
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        // Thay đổi vũ khí tương ứng với slot vừa chọn
        ChangeActiveWeapon();
    }

    /// <summary>
    /// Thay đổi vũ khí hiện tại của người chơi.
    /// </summary>
    private void ChangeActiveWeapon()
    {
        // Nếu đã có vũ khí được trang bị, hủy bỏ nó trước khi thay vũ khí mới
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        // Lấy thông tin vũ khí từ slot kho đồ hiện tại
        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();

        // Nếu slot hiện tại không chứa vũ khí, đặt trạng thái vũ khí null
        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        // Sinh vũ khí mới và đặt làm vũ khí hiện tại
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

        // Gán vũ khí mới vào ActiveWeapon
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
