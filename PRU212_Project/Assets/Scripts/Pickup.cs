using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Điều khiển hành vi của vật phẩm rơi (Pickup), bao gồm di chuyển về phía người chơi khi lại gần,
/// hiển thị hiệu ứng spawn và thực hiện chức năng tương ứng khi được nhặt.
/// </summary>
public class Pickup : MonoBehaviour
{
    /// <summary>
    /// Loại vật phẩm rơi, có thể là tiền vàng, cầu thể lực hoặc cầu máu.
    /// </summary>
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,
    }

    [SerializeField] private PickUpType pickUpType; // Loại vật phẩm hiện tại
    [SerializeField] private float pickUpDistance = 5f; // Khoảng cách để bắt đầu hút về phía người chơi
    [SerializeField] private float accelartionRate = .2f; // Gia tốc khi di chuyển về phía người chơi
    [SerializeField] private float moveSpeed = 3f; // Tốc độ di chuyển ban đầu
    [SerializeField] private AnimationCurve animCurve; // Đường cong animation khi vật phẩm rơi xuống
    [SerializeField] private float heightY = 1.5f; // Độ cao khi vật phẩm nảy lên khi spawn
    [SerializeField] private float popDuration = 1f; // Thời gian hiệu ứng spawn

    private Vector3 moveDir; // Hướng di chuyển
    private Rigidbody2D rb; // Thành phần vật lý để điều khiển di chuyển

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine()); // Hiệu ứng rơi xuống khi spawn
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position; // Lấy vị trí người chơi

        // Nếu người chơi ở trong phạm vi hút vật phẩm
        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized; // Hướng di chuyển về người chơi
            moveSpeed += accelartionRate; // Tăng tốc độ di chuyển
        }
        else
        {
            moveDir = Vector3.zero; // Dừng di chuyển nếu người chơi ở ngoài phạm vi
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed * Time.deltaTime; // Di chuyển vật phẩm một cách mượt mà
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Nếu người chơi chạm vào vật phẩm, thực hiện chức năng tương ứng
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject); // Hủy vật phẩm sau khi nhặt
        }
    }

    /// <summary>
    /// Hiệu ứng rơi xuống khi vật phẩm xuất hiện.
    /// </summary>
    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);
        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    /// <summary>
    /// Xác định loại vật phẩm và thực hiện hành động phù hợp khi người chơi nhặt vật phẩm.
    /// </summary>
    private void DetectPickupType()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold(); // Cộng thêm vàng cho người chơi
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer(); // Hồi máu cho người chơi
                break;
            case PickUpType.StaminaGlobe:
                Stamina.Instance.RefreshStamina(); // Hồi thể lực cho người chơi
                break;
        }
    }
}
