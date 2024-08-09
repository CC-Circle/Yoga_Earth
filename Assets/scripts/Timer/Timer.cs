using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int timeLimit = 10; // タイマーの上限時間
    private int currentTime = 0;

    [SerializeField] private GameObject tcp;

    void Start()
    {
        StartCoroutine(StartTimer());
    }

    void Update()
    {

    }

    IEnumerator StartTimer()
    {
        // currentTime が timeLimit に達するまでループ
        while (currentTime < timeLimit)
        {
            // 1秒待機
            yield return new WaitForSeconds(1);

            // 時間をカウント
            currentTime++;
            Debug.Log("Time: " + currentTime);
        }
        StopCoroutine(StartTimer());

        // タイマーが終了した時の処理
        Debug.Log("Time's up!");
        NextScene();
    }

    private async void NextScene()
    {
        TCP tcpScript = tcp.GetComponent<TCP>();
        await tcpScript.DisconnectFromServerAsync();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
