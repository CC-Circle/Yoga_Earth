using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int timeLimit = 10; // タイマーの上限時間
    private int currentTime = 0;

    [SerializeField] private GameObject tcp;

    public static bool isTimeUp = false;

    private TextMeshProUGUI textMeshPro;
    private SaveScore save_Score;

    void Start()
    {
        StartCoroutine(StartTimer());
        textMeshPro = GetComponent<TextMeshProUGUI>();
        save_Score = GetComponent<SaveScore>();
        if (textMeshPro != null)
        {
            textMeshPro.text = "Limit: " + timeLimit + "s";
        }
    }

    void Update()
    {
        if (camera_follo.isMovedCamera == true)
        {
            // スコア表示のカメラ移動が終わったら、Timer表示を消す
            textMeshPro.text = "";
        }

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
            //Debug.Log("Time: " + currentTime);
            textMeshPro.text = "Time: " + (timeLimit - currentTime) + "s";
        }
        StopCoroutine(StartTimer());


        // タイマーが終了した時の処理
        //Debug.Log("Time's up!");
        textMeshPro.text = "Time's up!";
        isTimeUp = true;
        save_Score.SaveNewScore((int)set_segment.top_position.y);  // ここで、スコアを保存する
        // ここで、スコア表示のカメラ移動のプログラムを呼び出す
        //NextScene();    // 今はタイトル画面に遷移するけど、これをスコア表示のカメラ移動のプログラムを呼び出すように変更すること
    }

    private async void NextScene()  // スコア表示のカメラ移動のプログラムを呼び出すように変更したら、消して良い
    {
        TCP tcpScript = tcp.GetComponent<TCP>();
        await tcpScript.DisconnectFromServerAsync();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
