using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;
using DG.Tweening;
public class Timer : MonoBehaviour
{
    [SerializeField] private int timeLimit = 10; // タイマーの上限時間
    private int currentTime = 0;
    [SerializeField] private TextMeshProUGUI timerText;
    //[SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image[] countdownImages;

    [SerializeField] private GameObject set_segment_obj;

    public static bool isTimeUp = false;

    public static bool isGameStart = false;

    [SerializeField] private GameObject sampleBGMObj;
    private HonbanBGM honbanBGM;

    [SerializeField] private Image DamageImg;

    private Camera mainCamera;

    [SerializeField] private GameObject appleSpawnerObj;

    private AppleSpawner appleSpawnerScript;

    [SerializeField] private GameObject appleTreeSpawnerObj;
    private AppleTreeSpawner appleTreeSpawnerScript;

    [SerializeField] private int appleTreeCnt = 10;
    [SerializeField] private int appleGrowInterval = 10;


    void Start()
    {
        foreach (var countdownImage in countdownImages)
        {
            if (countdownImage != null)
            {
                countdownImage.enabled = false;
            }
        }

        isTimeUp = false;
        isGameStart = false;
        StartCoroutine(StartTimer());

        set_segment set_segmentScript = set_segment_obj.GetComponent<set_segment>();
        set_segmentScript.enabled = false;

        honbanBGM = sampleBGMObj.GetComponent<HonbanBGM>();

        DamageImg.color = Color.clear;

        mainCamera = Camera.main;

        appleSpawnerScript = appleSpawnerObj.GetComponent<AppleSpawner>();
        appleTreeSpawnerScript = appleTreeSpawnerObj.GetComponent<AppleTreeSpawner>();
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
            PenaltyTime(10);
        }
        //もし+を押したら、時間を増やす
        if (Input.GetKeyDown(KeyCode.Plus))
        {
            AddBonusTime(10);
        }

        DamageImg.color = Color.Lerp(DamageImg.color, Color.clear, Time.deltaTime);
    }

    IEnumerator StartTimer()
    {
        for (int i = 3; i > 0; i--)
        {
            //countdownText.text = i.ToString();
            countdownImages[i].enabled = true;
            Debug.Log(i);
            yield return new WaitForSeconds(1);
            countdownImages[i].enabled = false;
        }
        //countdownText.text = "Start!";
        countdownImages[0].enabled = true;
        yield return new WaitForSeconds(1);
        //countdownText.text = "";
        countdownImages[0].enabled = false;
        isGameStart = true;
        set_segment set_segmentScript = set_segment_obj.GetComponent<set_segment>();
        set_segmentScript.enabled = true;

        int appleTime = 0;

        // currentTime が timeLimit に達するまでループ
        while (currentTime < timeLimit)
        {
            // 1秒待機
            yield return new WaitForSeconds(1);

            // 時間をカウント
            currentTime++;
            //Debug.Log("Time: " + currentTime);

            appleTime++;
            if (appleTime == appleGrowInterval)
            {
                appleSpawnerScript.CreateApple();
                appleTime = 0;
            }
        }
        StopCoroutine(StartTimer());

        // タイマーが終了した時の処理
        Debug.Log("Time's up!");
        Debug.Log("End AppleTreeCnt: " + AppleTreeSpawner.appleTreeCnt);
        honbanBGM.StopBGM();
        honbanBGM.PlayFinishSE();
        isTimeUp = true;
        yield return new WaitForSeconds(1.411f);
        honbanBGM.ChangeBGM();
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
        //Debug.Log("PenaltyTime");
        bool isApple = appleSpawnerScript.DeleteApple();
        if (isApple)
        {
            Shake(2, 2, 0.25f);
            DamageImg.color = new Color(0.5f, 1.0f, 0, 0.4f);
            appleTreeSpawnerScript.CreateAppleTree(appleTreeCnt);
            return;
        }
        DamageImg.color = new Color(0.7f, 0, 0, 0.7f);
        Shake(2, 2, 0.25f);
        StartCoroutine(TextColorRed());

        if (GetRemainingTime() - penaltyTime <= 0)
        {
            currentTime = timeLimit;
        }
        else
        {
            currentTime += penaltyTime;
        }
    }

    private IEnumerator TextColorRed()
    {
        // タイマーの色を赤色に変更
        timerText.color = Color.red;
        yield return new WaitForSeconds(1);
        timerText.color = Color.white;
    }

    public void Shake(float width, int count, float duration)
    {
        var camera = Camera.main.transform;
        var seq = DOTween.Sequence();
        // 振れ演出の片道の揺れ分の時間
        var partDuration = duration / count / 2f;
        // 振れ幅の半分の値
        var widthHalf = width / 2f;
        // 往復回数-1回分の振動演出を作る
        for (int i = 0; i < count - 1; i++)
        {
            seq.Append(camera.DOLocalRotate(new Vector3(-widthHalf, 0f), partDuration));
            seq.Append(camera.DOLocalRotate(new Vector3(widthHalf, 0f), partDuration));
        }
        // 最後の揺れは元の角度に戻す工程とする
        seq.Append(camera.DOLocalRotate(new Vector3(-widthHalf, 0f), partDuration));
        seq.Append(camera.DOLocalRotate(Vector3.zero, partDuration));
    }
}