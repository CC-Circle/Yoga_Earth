using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private int timeLimit = 10; // タイマーの上限時間
    private int currentTime = 0;

    [SerializeField] private GameObject tcp;
    [SerializeField] private TextMeshProUGUI timerText;

    void Start()
    {
        StartCoroutine(StartTimer());
    }

    void Update()
    {
        int remainingTime = GetRemainingTime();
        timerText.text = "Limit: " + remainingTime;
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
        // ここで、スコア表示のカメラ移動のプログラムを呼び出す
        NextScene();    // 今はタイトル画面に遷移するけど、これをスコア表示のカメラ移動のプログラムを呼び出すように変更すること
    }

    private async void NextScene()  // スコア表示のカメラ移動のプログラムを呼び出すように変更したら、消して良い
    {
        TCP tcpScript = tcp.GetComponent<TCP>();
        await tcpScript.DisconnectFromServerAsync();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public int GetRemainingTime()
    {
        return timeLimit - currentTime;
    }

    public void AddBonusTime(int addBonusTime)
    {
        currentTime -= addBonusTime;
    }

    public void PenaltyTime(int penaltyTime)
    {
        currentTime += penaltyTime;
    }
}
