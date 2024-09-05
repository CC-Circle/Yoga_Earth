using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AppleSpawner : MonoBehaviour
{
    private List<GameObject> appleSpawnPoints = new List<GameObject>();
    private List<AppleSpawnPoint> appleScripts = new List<AppleSpawnPoint>();

    [SerializeField] private GameObject applePrefab;

    void Start()
    {
        // 現在のオブジェクトのすべての子オブジェクトを取得
        foreach (Transform child in transform)
        {
            // 子オブジェクトをリストに追加
            appleSpawnPoints.Add(child.gameObject);

            // 子オブジェクトのAppleスクリプトをリストに追加
            appleScripts.Add(child.gameObject.GetComponent<AppleSpawnPoint>());
        }

        //printAppleSpawnPoints(appleSpawnPoints);
    }

    void Update()
    {

    }

    private void printAppleSpawnPoints(List<GameObject> appleSpawnPoints)
    {
        foreach (GameObject appleSpawnPoint in appleSpawnPoints)
        {
            Debug.Log(appleSpawnPoint.name);
        }
    }

    public void CreateApple()
    {
        // forループを使ってインデックスを取得する
        for (int i = 0; i < appleScripts.Count; i++)
        {
            AppleSpawnPoint appleScript = appleScripts[i];

            if (appleScript.GetIsApple() == false)
            {
                // この時の、appleScriptがアタッチされているappleSpawnPointを取得
                GameObject correspondingSpawnPoint = appleSpawnPoints[i];

                // Appleの成長状態を更新
                appleScript.SetIsApple(true);

                // Appleを生成
                GameObject appleInstance = Instantiate(applePrefab, appleScript.transform.position, Quaternion.identity);
                appleInstance.transform.SetParent(correspondingSpawnPoint.transform);
                appleInstance.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                Apple appleInstanceScript = appleInstance.GetComponent<Apple>();
                //StartCoroutine(appleInstanceScript.FollowAppleSpawnPoint(correspondingSpawnPoint));
                StartCoroutine(appleInstanceScript.ScaleOverTime(new Vector3(1.0f, 1.0f, 1.0f), 1.0f));

                // ループを抜ける
                break;
            }
        }
    }

    public bool DeleteApple()
    {
        for (int i = appleScripts.Count - 1; i >= 0; i--)
        {
            AppleSpawnPoint appleScript = appleScripts[i];

            if (appleScript.GetIsApple() == true)
            {
                GameObject correspondingSpawnPoint = appleSpawnPoints[i];
                // 親オブジェクトのすべての子オブジェクトを取得し、配列に変換
                GameObject[] childObjects = correspondingSpawnPoint.transform.Cast<Transform>()
                    .Select(child => child.gameObject) // 無名関数を使って子オブジェクトを選択
                    .ToArray(); // 配列に変換

                Rigidbody rb = childObjects[0].GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                StartCoroutine(childObjects[0].GetComponent<Apple>().WaitDeleteApple(2.0f));
                appleScript.SetIsApple(false);
                return true;
            }
        }
        return false;
    }
}
