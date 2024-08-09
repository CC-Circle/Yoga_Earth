using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// TCP通信を管理するクラスです。サーバーへの接続、メッセージの送信・受信、および接続の管理を行います。
/// </summary>
public class TCP : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    [Header("TCP設定")]
    [SerializeField] private string Host = "127.0.0.1";
    [SerializeField] private int Port = 12345;
    [SerializeField] private int RetryDelay = 2; // リトライの間隔（秒）
    [SerializeField] private int Timeout = 5000; // タイムアウトの時間（ミリ秒）
    private bool isConnecting = false;
    private bool isReceivingData = false;
    private CancellationTokenSource cts;

    static public int x_zahyo = 50;

    /// <summary>
    /// スクリプトの開始時にサーバーへの接続を試みます。
    /// </summary>
    private async void Start()
    {
        SetXzahyo(50);
        cts = new CancellationTokenSource();
        await ConnectToServerAsync(cts.Token);
    }

    /// <summary>
    /// サーバーへの接続を非同期で試みます。接続に失敗した場合、リトライします。
    /// </summary>
    /// <param name="token">キャンセルトークン。</param>
    private async Task ConnectToServerAsync(CancellationToken token)
    {
        isConnecting = true;
        while (isConnecting)
        {
            try
            {
                if (client != null)
                {
                    client.Close();
                    client.Dispose();
                }
                client = new TcpClient();
                await client.ConnectAsync(Host, Port);
                stream = client.GetStream();
                isConnecting = false;
                Debug.Log("サーバーに接続しました");

                // 現在のUnityのシーン番号を取得
                string sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex.ToString();
                // 接続成功時にサーバーにUnityのシーン番号を送信
                if (client != null && client.Connected)
                {
                    await SendMessageToServerAsync(sceneIndex, token);
                    Debug.Log("シーン番号を送信: " + sceneIndex);
                    // 受信待機を開始
                    if (!isReceivingData)
                    {
                        _ = ReceiveDataFromServerAsync(token); // コルーチンの呼び出し後、結果を無視
                        isReceivingData = true;
                    }
                }
            }
            catch (SocketException e)
            {
                Debug.LogWarning($"接続に失敗しました。リトライします... {e.Message}");
            }

            if (isConnecting) // catch ブロック外でリトライのための待機を行う
            {
                await Task.Delay(RetryDelay * 1000, token);
            }
        }
    }

    /// <summary>
    /// 更新処理中にサーバーへの接続状況をチェックし、接続が切れている場合は再接続を試みます。
    /// また、特定のキー入力に応じてメッセージをサーバーに送信します。
    /// </summary>
    private async void Update()
    {
        if (client == null || !client.Connected)
        {
            if (!isConnecting)
            {
                await ConnectToServerAsync(cts.Token);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            await SendMessageToServerAsync("1", cts.Token);
        }
    }

    /// <summary>
    /// サーバーからのデータを非同期で受信します。接続が切れた場合やタイムアウトが発生した場合は再接続を試みます。
    /// </summary>
    /// <param name="token">キャンセルトークン。</param>
    private async Task ReceiveDataFromServerAsync(CancellationToken token)
    {
        Debug.Log("受信処理が開始されました");
        byte[] responseData = new byte[1024];

        while (!token.IsCancellationRequested)
        {
            if (client == null || !client.Connected)
            {
                Debug.LogWarning("接続が切れました。再接続を試みます...");
                isReceivingData = false;
                isConnecting = true;

                // 再接続を試みる
                await ConnectToServerAsync(token);

                // 再接続後もこのループを続ける
                continue;
            }

            try
            {
                Task<int> readTask = Task.Run(() => stream.Read(responseData, 0, responseData.Length), token);
                Task timeoutTask = Task.Delay(Timeout, token);
                Task completedTask = await Task.WhenAny(readTask, timeoutTask);

                if (completedTask == readTask)
                {
                    int bytes = readTask.Result;
                    if (bytes > 0)
                    {
                        string responseMessage = Encoding.ASCII.GetString(responseData, 0, bytes);
                        Debug.Log("サーバーから受信: " + responseMessage);
                        if (responseMessage.Equals("exit"))
                        {
                            Debug.Log("サーバーから終了の合図が来ました: " + responseMessage);
                            Debug.Log("サーバーとの再接続を試みます...");
                            isReceivingData = false;
                            isConnecting = true;

                            // 再接続を試みる
                            await ConnectToServerAsync(token);

                            // 再接続後もこのループを続ける
                            continue;
                        }

                        // 受信したデータに応じた処理を行う
                        // もし受診したデータがint型の整数に変換できたら、その値を使って処理を行う
                        if (int.TryParse(responseMessage, out int value))
                        {
                            Debug.Log("受信したデータを処理します: " + value);

                            // ここに受信したデータに応じた処理を記述する
                            int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
                            if (sceneIndex == 1)
                            {
                                SetXzahyo(value);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("データ受信のタイムアウト");
                }
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning("データ受信処理がキャンセルされました");
                break;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"データ受信中にエラーが発生しました: {e.Message}");
                // 接続が切れた場合に再接続
                Debug.LogWarning("接続が切れました。再接続を試みます...");
                isReceivingData = false;
                isConnecting = true;
                await ConnectToServerAsync(token);
            }
        }

        Debug.Log("受信処理が終了しました");
    }

    public void SetXzahyo(int x)
    {
        x_zahyo = x;
    }

    public int GetXzahyo()
    {
        return x_zahyo;
    }

    /// <summary>
    /// サーバーにメッセージを非同期で送信し、応答を受信します。応答が不正な場合は再送信します。
    /// </summary>
    /// <param name="message">送信するメッセージ。</param>
    /// <param name="token">キャンセルトークン。</param>
    private async Task SendMessageToServerAsync(string message, CancellationToken token)
    {
        if (stream != null)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length, token);
            Debug.Log("メッセージ送信: " + message);

            // 応答を非同期で待つ
            byte[] responseData = new byte[1024];
            int bytes = 0;
            bool receivedResponse = false;

            try
            {
                Task<int> readTask = Task.Run(() => stream.Read(responseData, 0, responseData.Length), token);
                Task timeoutTask = Task.Delay(Timeout, token);
                Task completedTask = await Task.WhenAny(readTask, timeoutTask);

                if (completedTask == readTask)
                {
                    bytes = readTask.Result;
                    receivedResponse = bytes > 0;
                }
                else
                {
                    // タイムアウト
                    Debug.LogWarning("応答のタイムアウト");
                }
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning("応答受信処理がキャンセルされました");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"応答の受信中にエラーが発生しました: {e.Message}");
            }

            if (receivedResponse)
            {
                string responseMessage = Encoding.ASCII.GetString(responseData, 0, bytes);
                if (responseMessage.Equals(message + "0"))
                {
                    Debug.Log("サーバーからの応答: " + responseMessage);

                    // サーバーから適切な応答が来たら、受信待機を開始
                    Debug.Log("受信待機を開始します");
                    if (!isReceivingData)
                    {
                        _ = ReceiveDataFromServerAsync(token);  // 修正: ReceiveDataFromServerAsync の呼び出し
                        isReceivingData = true;
                    }
                }
                else
                {
                    Debug.LogWarning("サーバーからの不正な応答: " + responseMessage);
                    Debug.LogWarning("サーバーから不正な応答を受信しました。再送信します...");
                    // 応答が不正な場合に再送信
                    await SendMessageToServerAsync(message, token);
                }
            }
            else
            {
                Debug.LogWarning("サーバーからの応答がありませんでした。再送信します...");
                // 応答がなかった場合に再送信
                await SendMessageToServerAsync(message, token);
            }
        }
    }

    /// <summary>
    /// アプリケーションが終了する際に、サーバーとの接続を閉じ、リソースを解放します。
    /// </summary>
    private void OnApplicationQuit()
    {
        // クライアントの接続を閉じる
        if (stream != null) stream.Close();
        if (client != null) client.Close();

        // キャンセルトークンのキャンセル
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
        }
    }

    /// <summary>
    /// サーバーとの接続を非同期で切断します。
    /// </summary>
    /// <returns>完了するタスク。</returns>
    public async Task DisconnectFromServerAsync()
    {
        if (stream != null)
        {
            stream.Close();
            stream = null;
        }
        if (client != null)
        {
            client.Close();
            client = null;
        }
        isConnecting = false;
        isReceivingData = false;

        // キャンセルトークンのキャンセル
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null; // cts を null に設定して、再利用を防ぐ
        }
        Debug.Log("サーバーとの接続が終了しました");
        await Task.CompletedTask; // 非同期メソッドにしておく
    }
}
