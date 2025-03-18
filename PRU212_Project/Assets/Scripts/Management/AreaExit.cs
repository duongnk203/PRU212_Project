using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    // Tên của scene sẽ được load khi người chơi đi vào vùng này
    [SerializeField] private string sceneToLoad;

    // Tên của điểm chuyển cảnh, giúp xác định vị trí của người chơi sau khi vào scene mới
    [SerializeField] private string sceneTransitionName;

    // Thời gian chờ trước khi load scene (hiệu ứng fade-out)
    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu vật thể chạm vào có phải là nhân vật người chơi hay không
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // Lưu tên điểm chuyển cảnh để sử dụng khi vào scene mới
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);

            // Thực hiện hiệu ứng fade-out (màn hình đen dần)
            UIFade.Instance.FadeToBlack();

            // Bắt đầu coroutine chờ và load scene
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        // Chờ đến khi thời gian đếm ngược về 0 trước khi load scene
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        // Chuyển sang scene mới
        SceneManager.LoadScene(sceneToLoad);
    }
}
