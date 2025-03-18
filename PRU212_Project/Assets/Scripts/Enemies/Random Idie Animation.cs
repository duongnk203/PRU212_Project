using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake()
    {
        // Lấy thành phần Animator của đối tượng
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Nếu không có Animator, thoát khỏi hàm
        if (!myAnimator) { return; }

        // Lấy thông tin trạng thái hiện tại của Animator
        AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);

        // Chạy animation từ một thời điểm ngẫu nhiên để tạo sự khác biệt giữa các đối tượng
        myAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
