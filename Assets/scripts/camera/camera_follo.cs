using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class camera_follo : MonoBehaviour
{


    float count_x;
    private Camera mainCamera;

    [SerializeField] private GameObject set_segment_obj;
    //[SerializeField] private GameObject tcp;

    private float maxTreeHeight = 0.0f;
    private Vector3 targetPosition = new Vector3(0, 0, 0);

    private float duration = 5.0f;
    //public float overshoot = 1.70158f; // オーバーシュートの強さを調整
    public float overshoot = 10.0f; // オーバーシュートの強さを調整

    public static bool isMovedCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        isMovedCamera = false;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.isTimeUp == true)
        {
            //Debug.Log("Time's up!");

            // 木の最大の高さを取得
            maxTreeHeight = set_segment.top_position.y + 3.0f;

            // これ以上木が伸びないようにする
            set_segment set_segmentScript = set_segment_obj.GetComponent<set_segment>();
            set_segmentScript.enabled = false;
            //TCP.x_zahyo = -1;
            SampleUDP.x_zahyo = -1;

            // スコア表示のカメラ移動のプログラムを呼び出す
            MoveCameraForScoreDisplay();
        }
        else
        {
            transform.position = new Vector3(0f, set_segment.top_position.y + 1.5f, set_segment.top_position.z - 5f);
        }

        if (isMovedCamera == true)
        {
            // スコア表示のカメラ移動が終わったら、タイトル画面に遷移する
            isMovedCamera = false;
            Debug.Log("スコア表示のカメラ移動が終わった");
            StartCoroutine(ToTitleScene());
        }

    }

    private void MoveCameraForScoreDisplay()
    {
        float nearClipPlane = 0.0f;
        float farClipPlane = 0.0f;
        float drawingLimitZDistance = 0.0f;
        if (mainCamera != null)
        {
            // 近クリッピング平面の距離
            nearClipPlane = mainCamera.nearClipPlane;

            // 遠クリッピング平面の距離
            farClipPlane = mainCamera.farClipPlane;

            // 描画限界までのz軸の長さを計算
            drawingLimitZDistance = farClipPlane - nearClipPlane;

            // コンソールに距離を表示
            //Debug.Log("Near Clip Plane Distance: " + nearClipPlane);
            //Debug.Log("Far Clip Plane Distance: " + farClipPlane);
            //Debug.Log("Drawing Limit Z Distance: " + drawingLimitZDistance);
        }
        else
        {
            Debug.LogError("Main Camera not found");
        }
        float fov = mainCamera.fieldOfView;
        float halfThetaRad = fov / 2.0f * Mathf.Deg2Rad;
        float cameraToTreeDistance = (maxTreeHeight / 2.0f) / Mathf.Tan(halfThetaRad);

        float aspectRatio = mainCamera.aspect;
        float verticalFOVRad = mainCamera.fieldOfView * Mathf.Deg2Rad;
        // 水平視野角を計算
        float horizontalFOVRad = 2.0f * Mathf.Atan(Mathf.Tan(verticalFOVRad / 2.0f) * aspectRatio);
        float halfHorizontalFOV = horizontalFOVRad * Mathf.Rad2Deg / 2.0f;

        float X = cameraToTreeDistance * Mathf.Tan(halfHorizontalFOV * Mathf.Deg2Rad);

        // カメラの位置を設定
        //transform.position = new Vector3(X - 5.0f, maxTreeHeight / 2.0f - 0.5f, -cameraToTreeDistance);
        targetPosition = new Vector3(X - 5.0f, maxTreeHeight / 2.0f - 0.5f, -cameraToTreeDistance);
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        float timeElapsed = 0.0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = targetPosition;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);

            // カスタムイージング関数を使用してカメラの位置を計算
            float easeT = EaseOutBack(t);

            transform.position = Vector3.Lerp(startPosition, endPosition, easeT);
            yield return null;
        }

        // 確実に最終位置に到達する
        transform.position = endPosition;
        isMovedCamera = true;
    }

    IEnumerator ToTitleScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Title");
    }

    private static float EaseOutBack(float x)
    {
        float c1 = 3.0f;
        float c3 = c1 + 1.0f;

        return 1.0f + c3 * Mathf.Pow(x - 1.0f, 3) + c1 * Mathf.Pow(x - 1.0f, 2);
    }
}