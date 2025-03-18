using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lớp BaseSingleton kế thừa từ Singleton, có thể được sử dụng làm lớp cha cho các Singleton khác.
/// </summary>
public class BaseSingleton : Singleton<BaseSingleton>
{
    // Hiện tại, lớp này không có logic bổ sung nào.
    // Nó có thể được mở rộng trong tương lai để thêm các chức năng chung cho tất cả Singleton.
}
