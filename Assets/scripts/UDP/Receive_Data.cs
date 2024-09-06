using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;

public class Receive_Data : MonoBehaviour
{

    // ポート番号
    [SerializeField] private int port = 5005;

    // UDPクライアント
    private UdpClient server;

    // 受信したデータを保持するキュー
    private readonly Queue<string> receivedDataQueue = new();

    private static bool isCenterHuman = false;
    private static int CenterHumanCnt = 0;


    [SerializeField] private TextMeshProUGUI debugText;

    [SerializeField] private Image gauge1;
    [SerializeField] private Image gauge2;
    [SerializeField] private Image gauge3;
    [SerializeField] private Image gauge4;
    [SerializeField] private Image gauge5;

    private RectTransform rectTransform;

    private float gaugeValue = 0.0f;

    void Start()
    {
        isCenterHuman = false;
        CenterHumanCnt = 0;
        // サーバーのIPアドレスを取得
        IPAddress ipAddress = IPAddress.Any;

        // UDPクライアントの作成
        server = new UdpClient(port);
        IPEndPoint endPoint = new(ipAddress, port);

        //Debug.Log("UDPサーバーを起動しました");

        // データ受信の非同期処理を開始
        server.BeginReceive(ReceiveData, endPoint);

        // データ処理を非同期的に行うコルーチンを開始
        StartCoroutine(ProcessData());

        debugText.text = "0";

        gauge1.enabled = true;
        gauge2.enabled = false;
        gauge3.enabled = false;
        gauge4.enabled = false;
        gauge5.enabled = false;

        rectTransform = gauge1.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (gaugeValue >= 124)
        {
            Debug.Log("Center Human");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }
        if (isCenterHuman)
        {
            CenterHumanCnt++;
            gaugeValue += 0.5f;
            rectTransform.sizeDelta = new Vector2(40, gaugeValue);
            rectTransform.localPosition = new Vector3(2.3f, IncreaseGaugeLinear(gaugeValue), 0);
        }
        else
        {
            if (gaugeValue > 0.0f)
            {
                gaugeValue -= 0.01f;
            }
        }

        if (CenterHumanCnt % 100 == 0 && CenterHumanCnt != 0)
        {
            Debug.Log(CenterHumanCnt);
            debugText.text = CenterHumanCnt.ToString();
        }


        /*
        if (CenterHumanCnt == 100)
        {
            gauge1.enabled = true;
        }
        if (CenterHumanCnt == 200)
        {
            gauge2.enabled = true;
        }
        if (CenterHumanCnt == 300)
        {
            gauge3.enabled = true;
        }
        if (CenterHumanCnt == 400)
        {
            gauge4.enabled = true;
        }
        if (CenterHumanCnt == 500)
        {
            gauge5.enabled = true;
        }
        */

        //もし1キーが押されたら
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
        //もし2キーが押されたら
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }
        //もし3キーが押されたら
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Honban");
        }
    }

    private float IncreaseGaugeLinear(float x)
    {
        return 0.5f * x - 61.0f;
    }

    // データ受信時の処理
    private void ReceiveData(IAsyncResult result)
    {
        // データと送信元のIPアドレスを取得
        IPEndPoint endPoint = new(IPAddress.Any, port);
        byte[] receivedBytes = server.EndReceive(result, ref endPoint);
        string receivedMessage = Encoding.ASCII.GetString(receivedBytes);

        //Debug.Log("受信したデータ: " + receivedMessage);

        // 受信したデータをキューに追加
        lock (receivedDataQueue)
        {
            receivedDataQueue.Enqueue(receivedMessage);
        }

        // 再度データ受信の準備
        server.BeginReceive(ReceiveData, endPoint);
    }

    // データ処理のコルーチン
    private IEnumerator ProcessData()
    {
        while (true)
        {
            string data = null;

            // 受信データがあるか確認
            lock (receivedDataQueue)
            {
                if (receivedDataQueue.Count > 0)
                {
                    data = receivedDataQueue.Dequeue();
                }
            }

            // 受信データがあれば処理
            if (data != null)
            {
                if (int.Parse(data) != -1)
                {
                    isCenterHuman = true;
                    //Debug.Log("Center Human");
                }
                else
                {
                    isCenterHuman = false;
                }
            }

            // 1フレーム待機
            yield return null;
        }
    }

    void OnDestroy()
    {
        // UDPサーバーの終了
        server?.Close();
    }
}
