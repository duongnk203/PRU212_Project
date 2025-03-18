using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Điều khiển hành vi AI của kẻ địch: di chuyển ngẫu nhiên và tấn công khi phát hiện người chơi.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [Header("Roaming Settings")]
    [SerializeField] private float roamChangeDirFloat = 2f; // Thời gian đổi hướng khi di chuyển ngẫu nhiên

    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 0f; // Phạm vi tấn công
    [SerializeField] private MonoBehaviour enemyType; // Kiểu kẻ địch, phải implement IEnemy
    [SerializeField] private float attackCooldown = 2f; // Thời gian hồi giữa các đòn tấn công
    [SerializeField] private bool stopMovingWhileAttacking = false; // Kẻ địch có dừng lại khi tấn công không?

    private bool canAttack = true; // Kiểm tra xem có thể tấn công không

    private enum State
    {
        Roaming,    // Di chuyển ngẫu nhiên
        Attacking   // Đang tấn công người chơi
    }

    private Vector2 roamPosition; // Điểm đến khi di chuyển ngẫu nhiên
    private float timeRoaming = 0f; // Thời gian đã di chuyển ở trạng thái Roaming

    private State state; // Trạng thái hiện tại của kẻ địch
    private EnemyPathfinding enemyPathfinding; // Đối tượng điều khiển đường đi của kẻ địch

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming; // Mặc định kẻ địch bắt đầu ở trạng thái di chuyển ngẫu nhiên
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition(); // Xác định điểm đến ban đầu
    }

    private void Update()
    {
        MovementStateControl(); // Điều khiển trạng thái AI mỗi frame
    }

    /// <summary>
    /// Kiểm soát hành vi của kẻ địch dựa trên trạng thái hiện tại.
    /// </summary>
    private void MovementStateControl()
    {
        switch (state)
        {
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    /// <summary>
    /// Hành vi di chuyển ngẫu nhiên của kẻ địch.
    /// </summary>
    private void Roaming()
    {
        timeRoaming += Time.deltaTime; // Tăng thời gian đã di chuyển ở trạng thái này

        enemyPathfinding.MoveTo(roamPosition); // Điều hướng kẻ địch đến điểm random

        // Kiểm tra nếu người chơi vào phạm vi tấn công, chuyển sang trạng thái Attacking
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        // Sau một khoảng thời gian, thay đổi hướng di chuyển
        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    /// <summary>
    /// Hành vi tấn công người chơi của kẻ địch.
    /// </summary>
    private void Attacking()
    {
        // Nếu người chơi rời khỏi phạm vi tấn công, quay lại trạng thái Roaming
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        // Nếu có thể tấn công và đã trong phạm vi tấn công
        if (attackRange != 0 && canAttack)
        {
            canAttack = false; // Ngăn chặn việc tấn công liên tục
            (enemyType as IEnemy).Attack(); // Gọi hàm Attack() từ interface IEnemy

            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving(); // Dừng di chuyển khi tấn công
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition); // Tiếp tục di chuyển nếu không cần dừng
            }

            StartCoroutine(AttackCooldownRoutine()); // Bắt đầu thời gian hồi chiêu
        }
    }

    /// <summary>
    /// Coroutine xử lý thời gian hồi chiêu sau khi tấn công.
    /// </summary>
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // Cho phép tấn công lại sau thời gian hồi chiêu
    }

    /// <summary>
    /// Sinh điểm di chuyển ngẫu nhiên cho kẻ địch.
    /// </summary>
    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f; // Reset bộ đếm thời gian roaming
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; // Trả về vị trí ngẫu nhiên
    }
}
