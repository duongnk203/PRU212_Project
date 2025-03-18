using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update()
    {
        // Mỗi khung hình, gọi phương thức để xoay đối tượng hướng về con trỏ chuột
        FaceMouse();
    }

    private void FaceMouse()
    {
        // Lấy vị trí con trỏ chuột trên màn hình (tọa độ pixel)
        Vector3 mousePosition = Input.mousePosition;

        // Chuyển đổi tọa độ chuột từ không gian màn hình sang không gian thế giới game
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Tính vector hướng từ đối tượng đến con trỏ chuột
        Vector2 direction = transform.position - mousePosition;

        // Cập nhật hướng của đối tượng để nó luôn quay về phía con trỏ chuột
        transform.right = -direction;
    }
}
