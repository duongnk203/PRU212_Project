using System.Collections;
using UnityEngine;

/// <summary>
/// Quản lý hệ thống trang bị vũ khí từ kho đồ (Inventory).
/// Sử dụng Singleton để đảm bảo chỉ có một thể hiện duy nhất.
/// </summary>
public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0; // Chỉ số của slot đang được kích hoạt
    private PlayerControls playerControls; // Điều khiển bàn phím của người chơi

    private Coroutine rentalCoroutine; // Coroutine để kiểm soát thời gian thuê vũ khí

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
        int index = numValue - 1; // Chuyển về chỉ số trong danh sách (bắt đầu từ 0)

        if (index == 0)
        {
            ToggleActiveHighlight(index); // Vũ khí mặc định, không cần thuê
        }
        else
        {
            int rentalCost = GetWeaponRentalCost(index);
            int playerGold = EconomyManager.Instance.GetGold(); // Lấy số vàng từ EconomyManager

            if (playerGold >= rentalCost)
            {
                EconomyManager.Instance.SpendGold(rentalCost); // Trừ vàng trong EconomyManager
                Debug.Log($"Đã thuê vũ khí {index + 1} với giá {rentalCost} vàng. Số vàng còn lại: {EconomyManager.Instance.GetGold()}");

                ToggleActiveHighlight(index);

                if (rentalCoroutine != null)
                {
                    StopCoroutine(rentalCoroutine); // Nếu đang thuê vũ khí khác, hủy bộ đếm trước
                }

                rentalCoroutine = StartCoroutine(RentalWeaponTimer());
            }
            else
            {
                Debug.Log($"Không đủ vàng để thuê vũ khí {index + 1}. Cần {rentalCost} vàng.");
            }
        }
    }

    /// <summary>
    /// Xác định chi phí thuê vũ khí dựa trên chỉ số slot.
    /// </summary>
    private int GetWeaponRentalCost(int index)
    {
        if (index == 1) return 5;  // Vũ khí 2 giá 5 gold
        if (index == 2) return 10; // Vũ khí 3 giá 10 gold
        return 0; // Mặc định vũ khí đầu tiên miễn phí
    }

    /// <summary>
    /// Hiển thị hiệu ứng chọn slot vũ khí và thay đổi vũ khí.
    /// </summary>
    /// <param name="indexNum">Chỉ số của slot cần kích hoạt.</param>
    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    /// <summary>
    /// Thay đổi vũ khí hiện tại của người chơi.
    /// </summary>
    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    /// <summary>
    /// Bộ đếm thời gian thuê vũ khí (10 giây).
    /// Sau thời gian này, vũ khí sẽ bị gỡ bỏ và quay về vũ khí mặc định.
    /// </summary>
    private IEnumerator RentalWeaponTimer()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Hết thời gian thuê vũ khí. Quay về vũ khí mặc định.");
        ToggleActiveHighlight(0); // Trở về vũ khí mặc định
    }
}
