using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp generic Singleton giúp quản lý thể hiện duy nhất (instance) của một class kế thừa nó.
/// Đảm bảo chỉ có một instance duy nhất tồn tại trong game.
/// </summary>
/// <typeparam name="T">Kiểu class kế thừa Singleton.</typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    /// <summary>
    /// Thể hiện duy nhất của class.
    /// </summary>
    private static T instance;
    public static T Instance { get { return instance; } }

    /// <summary>
    /// Phương thức Awake được gọi khi đối tượng được khởi tạo.
    /// Đảm bảo rằng chỉ có một instance duy nhất tồn tại, nếu có nhiều hơn một, sẽ tự động hủy đối tượng thừa.
    /// </summary>
    protected virtual void Awake()
    {
        // Kiểm tra xem đã có instance nào tồn tại chưa
        if (instance != null && this.gameObject != null)
        {
            // Nếu đã có instance khác, hủy đối tượng mới để đảm bảo Singleton
            Destroy(this.gameObject);
        }
        else
        {
            // Nếu chưa có, gán instance hiện tại
            instance = (T)this;
        }

        // Nếu gameObject không có parent, giữ nó tồn tại qua các scene
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
