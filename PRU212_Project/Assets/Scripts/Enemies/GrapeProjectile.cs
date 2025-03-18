using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f; // Thời gian bay của đạn
    [SerializeField] private AnimationCurve animCurve; // Đường cong animation cho chuyển động của đạn
    [SerializeField] private float heightY = 3f; // Độ cao tối đa của đường bay
    [SerializeField] private GameObject grapeProjectileShadow; // Bóng của đạn
    [SerializeField] private GameObject splatterPrefab; // Hiệu ứng va chạm khi đạn chạm đất

    private void Start()
    {
        // Tạo bóng của đạn ngay khi đạn được bắn ra
        GameObject grapeShadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity);

        // Lấy vị trí của người chơi
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

        // Bắt đầu di chuyển đạn theo đường cong
        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        // Bắt đầu di chuyển bóng của đạn theo đường bay
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPosition, playerPos));
    }

    /// <summary>
    /// Coroutine giúp đạn bay theo một quỹ đạo cong.
    /// </summary>
    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration; // Tiến trình tuyến tính từ 0 -> 1
            float heightT = animCurve.Evaluate(linearT); // Lấy giá trị từ đường cong animation
            float height = Mathf.Lerp(0f, heightY, heightT); // Tính độ cao theo đường cong

            // Di chuyển đạn theo trục X, Y và thêm chiều cao để tạo hiệu ứng cong
            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }

        // Khi đạn chạm đất, tạo hiệu ứng va chạm và hủy đạn
        Instantiate(splatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// Coroutine giúp di chuyển bóng của đạn theo đường bay của nó.
    /// </summary>
    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration; // Tiến trình tuyến tính từ 0 -> 1
            grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT); // Di chuyển bóng theo đường thẳng
            yield return null;
        }

        // Khi đạn chạm đất, hủy bóng
        Destroy(grapeShadow);
    }
}
