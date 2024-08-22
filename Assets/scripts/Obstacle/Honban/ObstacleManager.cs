using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject floatingObstacleSpawner;
    FloatingObstacleSpawner floatingObstacleSpawnerScript;
    [SerializeField] private GameObject parallelMovingObstacleSpawner;
    ParallelMovingObstacleSpawner parallelMovingObstacleSpawnerScript;
    [SerializeField] private GameObject movingDiagonallyObstacleSpawner;
    MovingDiagonallyObstacleSpawner movingDiagonallyObstacleSpawnerScript;
    [SerializeField] private GameObject ComingFrontObstacleSpawner;
    ComingFrontObstacleSpawner comingFrontObstacleSpawnerScript;
    [SerializeField] private GameObject rainCloudObj;
    rain_cloud rainCloudScript;


    private bool isSpawnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        floatingObstacleSpawnerScript = floatingObstacleSpawner.GetComponent<FloatingObstacleSpawner>();
        parallelMovingObstacleSpawnerScript = parallelMovingObstacleSpawner.GetComponent<ParallelMovingObstacleSpawner>();
        movingDiagonallyObstacleSpawnerScript = movingDiagonallyObstacleSpawner.GetComponent<MovingDiagonallyObstacleSpawner>();
        comingFrontObstacleSpawnerScript = ComingFrontObstacleSpawner.GetComponent<ComingFrontObstacleSpawner>();
        rainCloudScript = rainCloudObj.GetComponent<rain_cloud>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.isGameStart && !isSpawnStart)
        {
            Debug.Log("ObstacleManager: Start spawning obstacles");
            isSpawnStart = true;
            StartCoroutine(floatingObstacleSpawnerScript.ExecuteAtRandomIntervals());
            StartCoroutine(parallelMovingObstacleSpawnerScript.ExecuteAtRandomIntervals());
            StartCoroutine(movingDiagonallyObstacleSpawnerScript.ExecuteAtRandomIntervals());
            StartCoroutine(comingFrontObstacleSpawnerScript.ExecuteAtRandomIntervals());
            StartCoroutine(rainCloudScript.ExecuteAtRandomIntervals());
        }
    }
}
