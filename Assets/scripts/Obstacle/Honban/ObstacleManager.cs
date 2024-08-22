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

    private bool isSpawnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        floatingObstacleSpawnerScript = floatingObstacleSpawner.GetComponent<FloatingObstacleSpawner>();
        parallelMovingObstacleSpawnerScript = parallelMovingObstacleSpawner.GetComponent<ParallelMovingObstacleSpawner>();
        movingDiagonallyObstacleSpawnerScript = movingDiagonallyObstacleSpawner.GetComponent<MovingDiagonallyObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.isGameStart && !isSpawnStart)
        {
            isSpawnStart = true;
            //StartCoroutine(floatingObstacleSpawnerScript.ExecuteAtRandomIntervals());
            //StartCoroutine(parallelMovingObstacleSpawnerScript.ExecuteAtRandomIntervals());
            StartCoroutine(movingDiagonallyObstacleSpawnerScript.ExecuteAtRandomIntervals());
        }
    }
}
