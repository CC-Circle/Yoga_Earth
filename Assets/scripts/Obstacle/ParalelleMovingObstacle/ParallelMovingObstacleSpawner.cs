using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParallelMovingObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject parallelMovingObstaclePrefab;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);
    [SerializeField] private float duration = 18.0f;
    [SerializeField] private float spawnInterval = 5.0f;
    [SerializeField] private float spawnProbability = 0.5f;

    private readonly System.Random random = new System.Random();

    void Start()
    {
        //StartCoroutine(ExecuteAtRandomIntervals());
    }

    private void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }

    private Vector3 GetSpawnPosition()
    {
        return this.spawnPosition;
    }

    private void SetRandomSpawnPosition()
    {
        float randomPositionXRight = 7.0f;
        float randomPositionXLeft = -7.0f;

        float selectedPositionX = GetRandomNumber(randomPositionXRight, randomPositionXLeft);

        // 将来的に、set_segment.top_position.yをgetTopPositionY()を使って取得するように変更
        SetSpawnPosition(new Vector3(selectedPositionX, set_segment.top_position.y + 3.0f, 0));

        if (selectedPositionX > 0)
        {
            StartMoveParallelMovingObstacle(new Vector3(randomPositionXLeft, GetSpawnPosition().y, 0));
        }
        else
        {
            StartMoveParallelMovingObstacle(new Vector3(randomPositionXRight, GetSpawnPosition().y, 0));
        }
    }

    // 2つの数値のうちどちらかをランダムで返すメソッド
    private float GetRandomNumber(float num1, float num2)
    {
        // 0または1をランダムに生成
        int randomIndex = random.Next(0, 2);
        return randomIndex == 0 ? num1 : num2;
    }

    private void StartMoveParallelMovingObstacle(Vector3 targetPosition)
    {
        if (parallelMovingObstaclePrefab != null)
        {
            GameObject obstacle = Instantiate(parallelMovingObstaclePrefab, GetSpawnPosition(), Quaternion.identity);
            if (GetSpawnPosition().x > 0)
            {
                obstacle.transform.Rotate(0, -90, 0);
            }
            else
            {
                obstacle.transform.Rotate(0, 90, 0);
            }
            ParallelMovingObstacle obstacleScript = obstacle.GetComponent<ParallelMovingObstacle>();
            if (obstacleScript != null)
            {
                obstacleScript.StartMoveParallelMovingObstacle(targetPosition, duration);
            }
            else
            {
                Debug.LogWarning("ParallelMovingObstacle script is not attached to the prefab.");
            }
        }
        else
        {
            Debug.LogWarning("ParallelMovingObstaclePrefab is not assigned in the inspector.");
        }
    }

    public IEnumerator ExecuteAtRandomIntervals()
    {
        while (true)
        {
            // 一定の間隔を待つ
            yield return new WaitForSeconds(spawnInterval);

            // 一定の確率で処理を実行
            if (UnityEngine.Random.value <= spawnProbability)
            {
                SetRandomSpawnPosition();
            }
        }
    }
}
