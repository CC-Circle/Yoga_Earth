using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingFrontObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject comingFrontObstaclePrefab;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);

    [SerializeField] private float spawnInterval = 5.0f;
    [SerializeField] private float spawnProbability = 0.5f;

    private Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(ExecuteAtRandomIntervals());
    }

    void Update()
    {

    }

    private void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }

    private Vector3 GetSpawnPosition()
    {
        return this.spawnPosition;
    }

    private float GetDrawingLimitZDistance()
    {
        float drawingLimitZDistance = 0.0f;
        if (mainCamera != null)
        {
            // 近クリッピング平面の距離
            float nearClipPlane = mainCamera.nearClipPlane;

            // 遠クリッピング平面の距離
            float farClipPlane = mainCamera.farClipPlane;

            // 描画限界までのz軸の長さを計算
            drawingLimitZDistance = farClipPlane - nearClipPlane;

            // コンソールに距離を表示
            //Debug.Log("Near Clip Plane Distance: " + nearClipPlane);
            //Debug.Log("Far Clip Plane Distance: " + farClipPlane);
            //Debug.Log("Drawing Limit Z Distance: " + drawingLimitZDistance);
        }
        else
        {
            //Debug.LogError("Main Camera not found");
        }

        return drawingLimitZDistance;
    }

    private void SetRomdomSpawnPosition()
    {
        float x = Random.Range(-5.0f, 5.0f);

        // 将来的に、set_segment.top_position.yをgetTopPositionY()を使って取得するように変更
        SetSpawnPosition(new Vector3(0, set_segment.top_position.y, GetDrawingLimitZDistance() - 200.0f));    // 生成位置を画面外にするためにy座標に7.0fを加算
    }

    private void SpownFloatingObstacle()
    {
        SetRomdomSpawnPosition();

        if (comingFrontObstaclePrefab != null)
        {
            Instantiate(comingFrontObstaclePrefab, GetSpawnPosition(), Quaternion.identity);

            //GameObject obstacle = Instantiate(comingFrontObstaclePrefab, GetSpawnPosition(), Quaternion.identity);
            //ComingFrontObstacle obstacleScript = obstacle.GetComponent<ComingFrontObstacle>();
            //if (obstacleScript != null)
            //{
            //obstacleScript.SetTargetPosition(new Vector3(0, 0, 0));
            //}
            //else
            //{
            //    Debug.LogWarning("Coming Front Obstacle Script is not attached to the obstacle prefab");
            //}
        }
        else
        {
            Debug.LogWarning("Coming Front Obstacle Prefab is not assigned in the inspector");
        }

    }

    public IEnumerator ExecuteAtRandomIntervals()
    {
        while (true)
        {
            // 一定の間隔を待つ
            yield return new WaitForSeconds(spawnInterval);

            // 一定の確率で処理を実行
            if (Random.value <= spawnProbability)
            {
                SpownFloatingObstacle();
                //Debug.Log("Spawned a Coming Front Obstacle");
            }
        }
    }
}
