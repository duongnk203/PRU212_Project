using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Điều khiển thanh kiếm (Sword), triển khai giao diện IWeapon.
/// Xử lý cơ chế tấn công, hiệu ứng chém, và hướng tấn công dựa trên vị trí chuột.
/// </summary>
public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab; // Prefab hiệu ứng chém
    [SerializeField] private Transform slashAnimSpawnPoint; // Vị trí xuất hiện hiệu ứng chém
    [SerializeField] private float swordAttackCD = .5f; // Thời gian hồi chiêu khi chém
    [SerializeField] private WeaponInfo weaponInfo; // Thông tin vũ khí

    private Transform weaponCollider; // Collider của vũ khí
    private Animator myAnimator; // Animator để xử lý hoạt ảnh chém

    private GameObject slashAnim; // Đối tượng hiệu ứng chém

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Lấy tham chiếu đến weaponCollider từ PlayerController
        weaponCollider = PlayerController.Instance.GetWeaponCollider();

        // Lấy vị trí xuất hiện hiệu ứng chém từ GameObject "SlashSpawnPoint"
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    /// <summary>
    /// Trả về thông tin vũ khí.
    /// </summary>
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    /// <summary>
    /// Thực hiện tấn công bằng kiếm.
    /// </summary>
    public void Attack()
    {
        myAnimator.SetTrigger("Attack"); // Kích hoạt animation chém
        weaponCollider.gameObject.SetActive(true); // Bật collider để kiểm tra va chạm

        // Tạo hiệu ứng chém tại vị trí đã xác định
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    /// <summary>
    /// Sự kiện khi hoạt ảnh chém kết thúc, vô hiệu hóa collider vũ khí.
    /// </summary>
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sự kiện khi vũ khí thực hiện động tác chém lên.
    /// Xoay hiệu ứng chém theo hướng thích hợp.
    /// </summary>
    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        // Nếu nhân vật đang quay mặt sang trái, lật hiệu ứng theo trục X
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    /// <summary>
    /// Sự kiện khi vũ khí thực hiện động tác chém xuống.
    /// Xoay hiệu ứng chém theo hướng thích hợp.
    /// </summary>
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        // Nếu nhân vật đang quay mặt sang trái, lật hiệu ứng theo trục X
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    /// <summary>
    /// Điều chỉnh hướng vũ khí theo vị trí của chuột để tạo cảm giác điều khiển mượt mà.
    /// </summary>
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition; // Lấy vị trí chuột trên màn hình
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) // Nếu chuột ở bên trái nhân vật
        {
            // Lật hướng vũ khí sang trái
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else // Nếu chuột ở bên phải nhân vật
        {
            // Giữ hướng bình thường
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
