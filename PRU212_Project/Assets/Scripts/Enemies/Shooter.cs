using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab; // Prefab của viên đạn
    [SerializeField] private float bulletMoveSpeed; // Tốc độ bay của đạn
    [SerializeField] private int burstCount; // Số lần bắn liên tiếp
    [SerializeField] private int projectilesPerBurst; // Số viên đạn mỗi lần bắn
    [SerializeField][Range(0, 359)] private float angleSpread; // Góc tỏa của đạn
    [SerializeField] private float startingDistance = 0.1f; // Khoảng cách ban đầu giữa viên đạn và người bắn
    [SerializeField] private float timeBetweenBursts; // Thời gian giữa mỗi lần bắn
    [SerializeField] private float restTime = 1f; // Thời gian nghỉ sau khi bắn xong
    [SerializeField] private bool stagger; // Có bắn theo kiểu giãn cách không?

    [Tooltip("Stagger phải được bật để oscillate hoạt động chính xác.")]
    [SerializeField] private bool oscillate; // Đạn có dao động qua lại giữa các góc bắn không?

    private bool isShooting = false; // Trạng thái có đang bắn hay không

    private void OnValidate()
    {
        // Đảm bảo giá trị hợp lệ khi thiết lập trên Inspector
        if (oscillate) { stagger = true; }
        if (!oscillate) { stagger = false; }
        if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistance < 0.1f) { startingDistance = 0.1f; }
        if (angleSpread == 0) { projectilesPerBurst = 1; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
    }

    /// <summary>
    /// Kích hoạt hành động tấn công của kẻ địch.
    /// </summary>
    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    /// <summary>
    /// Coroutine điều khiển cơ chế bắn nhiều viên đạn theo từng đợt.
    /// </summary>
    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        // Xác định góc bắn ban đầu
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst; }

        for (int i = 0; i < burstCount; i++) // Lặp lại theo số đợt bắn
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1) // Nếu có dao động, thay đổi góc bắn luân phiên
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < projectilesPerBurst; j++) // Tạo viên đạn theo từng đợt
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle); // Xác định vị trí bắn đạn

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position; // Xoay đạn theo hướng bắn

                // Cập nhật tốc độ đạn nếu có thành phần Projectile
                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep; // Cập nhật góc bắn tiếp theo

                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            if (!stagger) { yield return new WaitForSeconds(timeBetweenBursts); }
        }

        yield return new WaitForSeconds(restTime); // Nghỉ ngơi sau khi bắn xong
        isShooting = false;
    }

    /// <summary>
    /// Tính toán góc bắn để đảm bảo đạn tỏa ra theo một hình nón.
    /// </summary>
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;

        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    /// <summary>
    /// Xác định vị trí ban đầu của viên đạn dựa trên góc bắn.
    /// </summary>
    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        return new Vector2(x, y);
    }
}
