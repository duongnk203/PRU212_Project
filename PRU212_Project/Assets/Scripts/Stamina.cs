using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Quản lý hệ thống thể lực (stamina) của người chơi.
/// Stamina sẽ giảm khi sử dụng và tự động hồi phục sau một khoảng thời gian.
/// </summary>
public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; } // Lượng stamina hiện tại của người chơi

    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage; // Hình ảnh thể hiện stamina đầy hoặc cạn
    [SerializeField] private int timeBetweenStaminaRefresh = 3; // Thời gian hồi stamina (giây)

    private Transform staminaContainer; // Tham chiếu đến UI container chứa các biểu tượng stamina
    private int startingStamina = 3; // Stamina ban đầu
    private int maxStamina; // Giới hạn tối đa của stamina
    const string STAMINA_CONTAINER_TEXT = "Stamina Container"; // Tên object chứa UI stamina trong Scene

    /// <summary>
    /// Khởi tạo singleton và thiết lập stamina ban đầu.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina; // Đặt giá trị stamina tối đa
        CurrentStamina = startingStamina; // Đặt giá trị stamina hiện tại bằng stamina ban đầu
    }

    private void Start()
    {
        // Tìm kiếm UI container chứa stamina icons trong Scene
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    /// <summary>
    /// Giảm stamina khi người chơi sử dụng.
    /// </summary>
    public void UseStamina()
    {
        if (CurrentStamina > 0)
        {
            CurrentStamina--;
            UpdateStaminaImages();
        }
    }

    /// <summary>
    /// Hồi phục stamina nếu chưa đạt mức tối đa.
    /// </summary>
    public void RefreshStamina()
    {
        if (CurrentStamina < maxStamina)
        {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }

    /// <summary>
    /// Coroutine tự động hồi phục stamina theo thời gian.
    /// </summary>
    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    /// <summary>
    /// Cập nhật hình ảnh stamina trên UI.
    /// </summary>
    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i < CurrentStamina)
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            }
            else
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }
        }

        // Nếu chưa đầy stamina, dừng tất cả coroutine cũ và khởi động lại tiến trình hồi stamina
        if (CurrentStamina < maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
