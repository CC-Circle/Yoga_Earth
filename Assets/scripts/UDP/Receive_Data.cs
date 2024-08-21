using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

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
    }

    void Update()
    {
        if (CenterHumanCnt > 500)
        {
            Debug.Log("Center Human");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }
        if (isCenterHuman)
        {
            CenterHumanCnt++;
        }
        else
        {
            if (CenterHumanCnt > 100)
            {
                CenterHumanCnt -= 100;
            }
        }

        if (CenterHumanCnt % 100 == 0 && CenterHumanCnt != 0)
        {
            Debug.Log(CenterHumanCnt);
            debugText.text = CenterHumanCnt.ToString();
        }

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
