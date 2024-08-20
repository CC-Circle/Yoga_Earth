using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private int timeLimit = 10; // タイマーの上限時間
    private int currentTime = 0;
    [SerializeField] private TextMeshProUGUI timerText;

    public static bool isTimeUp = false;


    void Start()
    {
        isTimeUp = false;
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
        isTimeUp = true;
        //SaveScore saveScore = FindObjectOfType<SaveScore>();
        //saveScore.SaveNewScore((int)set_segment.top_position.y);
        // ここで、スコア表示のカメラ移動のプログラムを呼び出す
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