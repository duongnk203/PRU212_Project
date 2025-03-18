using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    // Tên của điểm chuyển cảnh, dùng để xác định người chơi có đi vào từ khu vực này hay không
    [SerializeField] private string transitionName;

    private void Start()
    {
        // Kiểm tra nếu tên của điểm chuyển cảnh trùng với tên chuyển cảnh hiện tại trong SceneManagement
        // Điều này có nghĩa là người chơi đã đi vào từ khu vực trước đó
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            // Đặt vị trí của nhân vật người chơi tại vị trí của AreaEntrance (điểm vào khu vực)
            PlayerController.Instance.transform.position = this.transform.position;

            // Cập nhật camera để theo dõi nhân vật người chơi sau khi dịch chuyển
            CameraController.Instance.SetPlayerCameraFollow();

            // Thực hiện hiệu ứng fade-in để màn hình dần hiện ra sau khi vào khu vực mới
            UIFade.Instance.FadeToClear();
        }
    }
}
