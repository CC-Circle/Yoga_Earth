using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject floatingObstaclePrefab;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);

    [SerializeField] private float spawnInterval = 5.0f;
    [SerializeField] private float spawnProbability = 0.5f;


    void Start()
    {
        //StartCoroutine(ExecuteAtRandomIntervals());
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

    private void SetRomdomSpawnPosition()
    {
        float x = Random.Range(-5.0f, 5.0f);

        // 将来的に、set_segment.top_position.yをgetTopPositionY()を使って取得するように変更
        SetSpawnPosition(new Vector3(x, set_segment.top_position.y + 7.0f, 0));    // 生成位置を画面外にするためにy座標に7.0fを加算
    }

    private void SpownFloatingObstacle()
    {
        SetRomdomSpawnPosition();
        if (floatingObstaclePrefab != null)
        {
            Instantiate(floatingObstaclePrefab, GetSpawnPosition(), Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Floating Obstacle Prefab is not assigned in the inspector");
        }
    }

    public IEnumerator ExecuteAtRandomIntervals()
    {
        while (true)
        {
            // 一定の確率で処理を実行
            if (Random.value <= spawnProbability)
            {
                SpownFloatingObstacle();
                //Debug.Log("Spawned a floating obstacle");
            }
            // 一定の間隔を待つ
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
