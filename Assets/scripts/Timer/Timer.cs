using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;

public class Timer : MonoBehaviour
{
    [SerializeField] private int timeLimit = 10; // タイマーの上限時間
    private int currentTime = 0;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private GameObject set_segment_obj;

    public static bool isTimeUp = false;

    public static bool isGameStart = false;


    void Start()
    {
        isTimeUp = false;
        isGameStart = false;
        StartCoroutine(StartTimer());

        set_segment set_segmentScript = set_segment_obj.GetComponent<set_segment>();
        set_segmentScript.enabled = false;
    }

    void Update()
    {
        int remainingTime = GetRemainingTime();
        if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        else
        {
            timerText.text = "Limit: " + remainingTime;
        }

        //もし-を押したら、時間を減らす
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            PenaltyTime(3);
        }
    }

    IEnumerator Countdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator StartTimer()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "Start!";
        yield return new WaitForSeconds(1);
        countdownText.text = "";
        isGameStart = true;
        set_segment set_segmentScript = set_segment_obj.GetComponent<set_segment>();
        set_segmentScript.enabled = true;

        // currentTime が timeLimit に達するまでループ
        while (currentTime < timeLimit)
        {
            // 1秒待機
            yield return new WaitForSeconds(1);

            // 時間をカウント
            currentTime++;
            //Debug.Log("Time: " + currentTime);
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
        Debug.Log("PenaltyTime");
        if (GetRemainingTime() - penaltyTime <= 0)
        {
            currentTime = timeLimit;
        }
        else
        {
            currentTime += penaltyTime;
        }
    }
}