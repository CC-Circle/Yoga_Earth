using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rain_cloud : MonoBehaviour
{
    [SerializeField] private GameObject floatingObstaclePrefab;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);

    [SerializeField] private float spawnInterval = 5.0f;
    [SerializeField] private float spawnProbability = 0.5f;

    GameObject cl_obj;//雲（アクセス用格納）　

    [SerializeField] float waittime = 7f;//雨が降り出すまでの時間

    [SerializeField] float durasion_rain = 10f;//雨が降り続ける時間

    [SerializeField] float dissolve_start_time = 0f;//雨が降り終わってから消えるまでの時間

    [SerializeField] float effectDuration = 3f;//雲が消える時間（値が大きいほどゆっくり消える）


    void Start()
    {
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

    private void SetRomdomSpawnPosition()
    {
        float x = Random.Range(-2.8f, 2.8f);
        //float Float_x= x*2.8f;

        // 将来的に、set_segment.top_position.yをgetTopPositionY()を使って取得するように変更
        SetSpawnPosition(new Vector3(x, set_segment.top_position.y + 9.0f, 0));    
    }

    private void SpownFloatingObstacle()
    {
        SetRomdomSpawnPosition();
        if (floatingObstaclePrefab != null)
        {
            cl_obj = Instantiate(floatingObstaclePrefab, GetSpawnPosition(), Quaternion.identity);
            //cl_obj.AddComponent<>();
        }
        else
        {
            Debug.LogWarning("Floating Obstacle Prefab is not assigned in the inspector");
        }
    }

    IEnumerator ExecuteAtRandomIntervals()
    {
        while (true)
        {
            // 一定の間隔を待つ
            yield return new WaitForSeconds(spawnInterval);

            // 一定の確率で処理を実行
            if (Random.value <= spawnProbability)
            {
                SpownFloatingObstacle();
                spawnProbability = 0;
                //Debug.Log("Spawned a floating obstacle");
            }
            else
            {
                spawnProbability = spawnProbability + spawnProbability*0.1f + 0.2f;
            }
        }
    }

    
}
