using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp PlayerController kế thừa từ Singleton, đảm bảo chỉ có một instance duy nhất của player trong game.
public class PlayerController : Singleton<PlayerController>
{
    // Biến kiểm tra hướng của nhân vật (true = quay trái, false = quay phải)
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f; // Tốc độ di chuyển bình thường của nhân vật
    [SerializeField] private float dashSpeed = 4f; // Hệ số tốc độ khi Dash
    [SerializeField] private TrailRenderer myTrailRenderer; // Hiệu ứng khi Dash
    [SerializeField] private Transform weaponCollider; // Vị trí collider của vũ khí

    private PlayerControls playerControls; // Input System của người chơi
    private Vector2 movement; // Vector lưu hướng di chuyển
    private Rigidbody2D rb; // Rigidbody2D để di chuyển nhân vật
    private Animator myAnimator; // Animator để điều khiển animation
    private SpriteRenderer mySpriteRender; // SpriteRenderer để điều khiển hướng nhân vật
    private Knockback knockback; // Hệ thống knockback (bật lùi)
    private float startingMoveSpeed; // Lưu giá trị tốc độ ban đầu

    private bool facingLeft = false; // Kiểm tra nhân vật có đang quay trái không
    private bool isDashing = false; // Kiểm tra nhân vật có đang Dash không

    // Hàm Awake chạy đầu tiên khi object được tạo, dùng để khởi tạo các thành phần cần thiết
    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls(); // Khởi tạo hệ thống điều khiển người chơi
        rb = GetComponent<Rigidbody2D>(); // Lấy Rigidbody2D của nhân vật
        myAnimator = GetComponent<Animator>(); // Lấy Animator
        mySpriteRender = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer
        knockback = GetComponent<Knockback>(); // Lấy component Knockback
    }

    // Hàm Start chạy khi object được kích hoạt lần đầu tiên
    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash(); // Gán sự kiện Dash khi nhấn phím

        startingMoveSpeed = moveSpeed; // Lưu lại tốc độ ban đầu

        ActiveInventory.Instance.EquipStartingWeapon(); // Trang bị vũ khí ban đầu cho người chơi
    }

    private void OnEnable()
    {
        playerControls.Enable(); // Kích hoạt điều khiển khi player được bật
    }

    private void OnDisable()
    {
        playerControls.Disable(); // Vô hiệu hóa điều khiển khi player bị tắt
    }

    private void Update()
    {
        PlayerInput(); // Xử lý input của người chơi
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection(); // Kiểm tra hướng của nhân vật theo con trỏ chuột
        Move(); // Xử lý di chuyển nhân vật
    }

    // Hàm trả về vị trí của weaponCollider để sử dụng trong hệ thống combat
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    // Xử lý đầu vào của người chơi (di chuyển)
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); // Đọc giá trị di chuyển từ input

        // Cập nhật animation dựa trên hướng di chuyển
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    // Xử lý di chuyển nhân vật
    private void Move()
    {
        // Nếu nhân vật đang bị knockback hoặc đã chết thì không cho di chuyển
        if (knockback.GettingKnockedBack || PlayerHealth.Instance.isDead) { return; }

        // Di chuyển nhân vật theo hướng input với tốc độ đã được xác định
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    // Điều chỉnh hướng nhân vật dựa vào vị trí chuột
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition; // Lấy vị trí của con trỏ chuột trên màn hình
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position); // Lấy vị trí của nhân vật trên màn hình

        // Nếu chuột ở bên trái nhân vật, lật sprite sang trái
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            facingLeft = true;
        }
        else // Nếu chuột ở bên phải, lật sprite sang phải
        {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }

    // Xử lý Dash của nhân vật
    private void Dash()
    {
        // Nếu đang Dash hoặc không đủ Stamina thì không thực hiện Dash
        if (!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina(); // Tiêu hao Stamina khi Dash
            isDashing = true; // Đánh dấu là đang Dash
            moveSpeed *= dashSpeed; // Tăng tốc độ di chuyển
            myTrailRenderer.emitting = true; // Bật hiệu ứng Dash
            StartCoroutine(EndDashRoutine()); // Bắt đầu Coroutine để kết thúc Dash
        }
    }

    // Coroutine kết thúc Dash sau một khoảng thời gian
    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f; // Thời gian Dash
        float dashCD = .25f; // Thời gian hồi chiêu Dash

        yield return new WaitForSeconds(dashTime); // Chờ hết thời gian Dash

        moveSpeed = startingMoveSpeed; // Reset lại tốc độ di chuyển
        myTrailRenderer.emitting = false; // Tắt hiệu ứng Dash

        yield return new WaitForSeconds(dashCD); // Chờ hồi chiêu

        isDashing = false; // Cho phép Dash lại
    }
}
