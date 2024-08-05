using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDiagonallyObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject movingDiagonallyObstaclePrefab;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);
    [SerializeField] private float duration = 13.0f;
    [SerializeField] private float spawnInterval = 5.0f;
    [SerializeField] private float spawnProbability = 0.5f;

    private readonly System.Random random = new System.Random();

    void Start()
    {
        StartCoroutine(ExecuteAtRandomIntervals());
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

        float randomPositionYUpper = 8.0f;
        float randomPositionYUnder = 0.0f;

        float selectedPositionX = GetRandomNumber(randomPositionXRight, randomPositionXLeft);
        float selectedPositionY = GetRandomNumber(randomPositionYUpper, randomPositionYUnder);

        // 将来的に、set_segment.top_position.yをgetTopPositionY()を使って取得するように変更
        SetSpawnPosition(new Vector3(selectedPositionX, set_segment.top_position.y + selectedPositionY, 0));

        if (selectedPositionX > 0)
        {
            if (selectedPositionY > 0)
            {
                StartMovingDiagonallyObstacle(new Vector3(randomPositionXLeft, GetSpawnPosition().y - randomPositionYUpper, 0));
            }
            else
            {
                StartMovingDiagonallyObstacle(new Vector3(randomPositionXLeft, GetSpawnPosition().y + randomPositionYUpper, 0));
            }
        }
        else
        {
            if (selectedPositionY > 0)
            {
                StartMovingDiagonallyObstacle(new Vector3(randomPositionXRight, GetSpawnPosition().y - randomPositionYUpper, 0));
            }
            else
            {
                StartMovingDiagonallyObstacle(new Vector3(randomPositionXRight, GetSpawnPosition().y + randomPositionYUpper, 0));
            }
        }
    }

    // 2つの数値のうちどちらかをランダムで返すメソッド
    private float GetRandomNumber(float num1, float num2)
    {
        // 0または1をランダムに生成
        int randomIndex = random.Next(0, 2);
        return randomIndex == 0 ? num1 : num2;
    }

    private void StartMovingDiagonallyObstacle(Vector3 targetPosition)
    {
        if (movingDiagonallyObstaclePrefab != null)
        {
            GameObject obstacle = Instantiate(movingDiagonallyObstaclePrefab, GetSpawnPosition(), Quaternion.identity);
            MovingDiagonallyObstacle obstacleScript = obstacle.GetComponent<MovingDiagonallyObstacle>();
            if (obstacleScript != null)
            {
                obstacleScript.StartMoveMovingDiagonallyObstacle(targetPosition, duration);
            }
            else
            {
                Debug.LogWarning("MovingDiagonallyObstacle script is not attached to the prefab.");
            }
        }
        else
        {
            Debug.LogWarning("MovingDiagonallyObstaclePrefab is not assigned in the inspector.");
        }
    }

    private IEnumerator ExecuteAtRandomIntervals()
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
