using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f; // Thời gian để laser mở rộng hoàn toàn

    private bool isGrowing = true; // Biến kiểm soát laser có tiếp tục mở rộng hay không
    private float laserRange; // Độ dài tối đa của laser
    private SpriteRenderer spriteRenderer; // Tham chiếu đến SpriteRenderer của laser
    private CapsuleCollider2D capsuleCollider2D; // Tham chiếu đến Collider của laser

    private void Awake()
    {
        // Lấy các component cần thiết khi khởi tạo
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        // Xoay laser về phía con trỏ chuột khi mới được tạo
        LaserFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Nếu laser chạm vào vật thể không thể phá hủy, dừng việc mở rộng
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger)
        {
            isGrowing = false;
        }
    }

    public void UpdateLaserRange(float laserRange)
    {
        // Cập nhật phạm vi tối đa của laser
        this.laserRange = laserRange;
        // Bắt đầu quá trình mở rộng laser
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f; // Biến theo dõi thời gian đã trôi qua

        while (spriteRenderer.size.x < laserRange && isGrowing) // Kiểm tra nếu laser chưa đạt phạm vi tối đa và vẫn đang mở rộng
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime; // Tính tỉ lệ thời gian đã trôi qua

            // Cập nhật kích thước của SpriteRenderer (tăng chiều dài laser)
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            // Cập nhật kích thước của Collider để khớp với Sprite
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, capsuleCollider2D.offset.y);

            yield return null; // Đợi frame tiếp theo
        }

        // Khi laser đạt phạm vi tối đa hoặc bị chặn, kích hoạt hiệu ứng mờ dần
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        // Lấy vị trí con trỏ chuột trong thế giới game
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Tính hướng từ laser đến con trỏ chuột
        Vector2 direction = transform.position - mousePosition;

        // Xoay laser theo hướng đó
        transform.right = -direction;
    }
}
