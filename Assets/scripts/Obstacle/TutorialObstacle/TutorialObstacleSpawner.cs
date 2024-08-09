using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tcp;

    [SerializeField] private GameObject tutorialObstaclePrefab;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);

    [SerializeField] private float spawnInterval = 7.0f;
    [SerializeField] private float spawnProbability = 0.5f;

    // チュートリアルの進捗状況に応じて、次の処理を実行する
    // tutorialProgressStatus: 0 -> チュートリアル開始
    // tutorialProgressStatus: 1 -> 木を伸ばす
    // tutorialProgressStatus: 2 -> 木を左右に動かす
    // tutorialProgressStatus: 3 -> 障害物を避ける
    // tutorialProgressStatus: 4 -> チュートリアル終了
    private int tutorialProgressStatus = 0;

    private float progressCnt_1 = 0.0f;

    private bool isLeftTargetReached = false;
    private bool isRightTargetReached = false;

    private Vector3 leftTargetPoint = new Vector3(-3.0f, 0, 0);
    private Vector3 rightTargetPoint = new Vector3(3.0f, 0, 0);

    private int tutorialObstacle_Cnt = 0;

    private float progressCnt4 = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        // チュートリアルを開始する: 0 -> 1
        StartTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        // スペースキーが押されたら、チュートリアルをスキップする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipTutorial();
        }

        if (tutorialProgressStatus == 1)
        {
            // チュートリアルの進捗状況が1の場合、木を伸ばす
            // 5秒間連続で、TCP.xzahyoが-1でない場合、次の進捗状況に進む
            if (TCP.x_zahyo != -1)
            {
                progressCnt_1 += Time.deltaTime;
                if (progressCnt_1 >= 5.0f)
                {
                    tutorialProgressStatus = 2;
                    Debug.Log("Next tutorial progress -> 2");
                }
            }
            else
            {
                progressCnt_1 = 0.0f;
            }
        }
        else if (tutorialProgressStatus == 2)
        {
            // チュートリアルの進捗状況が2の場合、木を左右に動かす
            // 左右の画面端に設定された、目標地点にset_segment.top_position.xが到達したら、次の進捗状況に進む
            if (isLeftTargetReached && isRightTargetReached)
            {
                tutorialProgressStatus = 3;
                Debug.Log("Next tutorial progress -> 3");
            }

            if (set_segment.top_position.x <= leftTargetPoint.x)
            {
                isLeftTargetReached = true;
                Debug.Log("Left target reached");
            }
            if (set_segment.top_position.x >= rightTargetPoint.x)
            {
                isRightTargetReached = true;
                Debug.Log("Right target reached");
            }
        }
        else if (tutorialProgressStatus == 3)
        {
            // チュートリアルの進捗状況が3の場合、障害物を避ける
            // 一定の間隔で、チュートリアル用の障害物を生成する.
            // 12秒間障害物を生成し続けたら、次の進捗状況に進む
            if (tutorialObstacle_Cnt == 0)
            {
                StartCoroutine(StartTutorialSpown());
            }

            if (tutorialObstacle_Cnt >= 1)
            {
                StopCoroutine(StartTutorialSpown());
                tutorialProgressStatus = 4;
                Debug.Log("Next tutorial progress -> 4");
            }

        }
        else if (tutorialProgressStatus == 4)
        {
            // チュートリアルが終了したら、次のシーンに遷移する
            progressCnt4 += Time.deltaTime;
            if (progressCnt4 >= 20.0f)
            {
                NextScene();
                progressCnt4 = -100.0f;
            }
        }
    }



    IEnumerator StartTutorialSpown()
    {
        while (true)
        {

            SetSpawnPosition(new Vector3(Random.Range(-5.0f, 5.0f), set_segment.top_position.y + 7.0f, 0));
            tutorialObstacle_Cnt++;
            if (tutorialObstacle_Cnt <= 1)
            {
                SpownTutorialObstacle();
            }
            //Debug.Log("Spawned a tutorial obstacle");

            // 一定の間隔を待つ
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpownTutorialObstacle()
    {
        if (tutorialObstaclePrefab != null)
        {
            Instantiate(tutorialObstaclePrefab, GetSpawnPosition(), Quaternion.identity);
            Debug.Log("Spawned a tutorial obstacle");
        }
        else
        {
            Debug.LogWarning("Tutorial Obstacle Prefab is not assigned in the inspector");
        }
    }

    IEnumerator ExecuteAtRandomIntervals()
    {
        while (true)
        {
            // 一定の間隔を待つ
            yield return new WaitForSeconds(spawnInterval);

            SpownTutorialObstacle();
            //Debug.Log("Spawned a tutorial obstacle");
        }
    }

    private void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }

    private Vector3 GetSpawnPosition()
    {
        return this.spawnPosition;
    }

    private void StartTutorial()
    {
        Debug.Log("Tutorial started");
        tutorialProgressStatus = 1;
    }

    private void SkipTutorial()
    {
        // チュートリアルをスキップする
        Debug.Log("Tutorial skipped");

        // チュートリアルの進捗状況を最終段階まで進める
        tutorialProgressStatus = 4;
    }

    private async void NextScene()
    {
        // サーバーとの接続を切断する
        TCP tcpScript = tcp.GetComponent<TCP>();
        await tcpScript.DisconnectFromServerAsync();

        // チュートリアルをスキップした後、次のシーンに遷移する
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
