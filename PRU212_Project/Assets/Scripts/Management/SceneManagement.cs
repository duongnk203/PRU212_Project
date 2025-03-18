using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý việc chuyển đổi giữa các cảnh (scene) trong game.
/// </summary>
public class SceneManagement : Singleton<SceneManagement>
{
    /// <summary>
    /// Tên của hiệu ứng chuyển cảnh hiện tại.
    /// Dùng để xác định trạng thái chuyển cảnh giữa các khu vực trong game.
    /// </summary>
    public string SceneTransitionName { get; private set; }

    /// <summary>
    /// Đặt tên cho hiệu ứng chuyển cảnh.
    /// </summary>
    /// <param name="sceneTransitionName">Tên của cảnh chuyển tiếp.</param>
    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }
}
