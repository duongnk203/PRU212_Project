using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Điều khiển đạn bắn ra, bao gồm di chuyển, va chạm và phạm vi bay.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f; // Tốc độ di chuyển của đạn
    [SerializeField] private GameObject particleOnHitPrefabVFX; // Hiệu ứng va chạm khi đạn chạm mục tiêu
    [SerializeField] private bool isEnemyProjectile = false; // Xác định đạn này có phải của kẻ địch không
    [SerializeField] private float projectileRange = 10f; // Phạm vi bay tối đa của đạn

    private Vector3 startPosition; // Vị trí ban đầu của đạn

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();      // Di chuyển đạn về phía trước
        DetectFireDistance();  // Kiểm tra khoảng cách bay
    }

    /// <summary>
    /// Cập nhật phạm vi bay của đạn.
    /// </summary>
    /// <param name="projectileRange">Phạm vi bay mới.</param>
    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    /// <summary>
    /// Cập nhật tốc độ bay của đạn.
    /// </summary>
    /// <param name="moveSpeed">Tốc độ bay mới.</param>
    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    /// <summary>
    /// Xử lý khi đạn va chạm với vật thể khác.
    /// </summary>
    /// <param name="other">Collider của vật thể bị va chạm.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Lấy các thành phần cần kiểm tra
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        // Xử lý va chạm nếu vật thể không phải trigger và là mục tiêu hợp lệ
        if (!other.isTrigger && (enemyHealth || indestructible || player))
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform); // Gây sát thương cho người chơi nếu đạn của kẻ địch
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation); // Hiển thị hiệu ứng va chạm
                Destroy(gameObject); // Hủy đạn sau khi va chạm
            }
            else if (indestructible) // Nếu va chạm với vật không thể phá hủy
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Kiểm tra nếu đạn bay quá xa so với vị trí ban đầu thì sẽ bị hủy.
    /// </summary>
    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Di chuyển đạn về phía trước.
    /// </summary>
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
