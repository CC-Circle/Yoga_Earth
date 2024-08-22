using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDiagonallyObstacle : MonoBehaviour
{
    // メインカメラの外にいると判断されるまでの時間（秒）
    [SerializeField] private float timeOutsideViewThreshold = 1.0f;

    // メインカメラの外にいる時間をカウントするための変数
    private float timeOutsideView = 0.0f;

    // ゲームオブジェクトが視界外にいるかどうかを追跡するフラグ
    private bool isOutsideView = false;

    [SerializeField] float moveSpeed = 1.0f;

    private GameObject TimerObj;
    Timer timerScript;



    void Start()
    {
        //Debug.Log("FloatingObstacle script is attached to " + gameObject.name);
        TimerObj = GameObject.Find("Timer");
        timerScript = TimerObj.GetComponent<Timer>();
    }

    void Update()
    {
        if (isOutsideView)
        {
            // カメラの外にいる時間をカウント
            timeOutsideView += Time.deltaTime;

            // 一定時間以上カメラの外にいた場合、ゲームオブジェクトを削除
            if (timeOutsideView >= timeOutsideViewThreshold)
            {
                Destroy(gameObject);
                //Debug.Log("Destroyed object: " + gameObject.name);
            }
        }
        if (Timer.isTimeUp)
        {
            Destroy(gameObject);
        }
    }

    public void StartMoveMovingDiagonallyObstacle(Vector3 targetPosition, float time)
    {
        //Debug.Log("StartMoveParallelMovingObstacle");
        StartCoroutine(MoveMovingDiagonallyObstacle(targetPosition, time));
    }

    private IEnumerator MoveMovingDiagonallyObstacle(Vector3 targetPosition, float time)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;
        while (elapsedTime < time)
        {
            // 移動位置を計算し設定
            transform.position = Vector3.MoveTowards(startPosition, targetPosition, (elapsedTime / time) * Vector3.Distance(startPosition, targetPosition));

            // 経過時間を更新
            elapsedTime += Time.deltaTime;
            // 目標位置に到達したか確認
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition; // 目標位置に正確に設定
                yield break; // コルーチンを終了
            }
            yield return null;
        }

        // 最終的に目標位置に到達
        transform.position = targetPosition;
    }



    // ゲームオブジェクトがカメラの視野内に入った時に呼び出される
    void OnBecameVisible()
    {
        isOutsideView = false;
        timeOutsideView = 0.0f; // タイマーをリセット
    }

    // ゲームオブジェクトがカメラの視野外に出た時に呼び出される
    void OnBecameInvisible()
    {
        isOutsideView = true;
    }

    void OnTriggerEnter(Collider Collider)
    {
        //Debug.Log("接触したオブジェクト：" + gameObject.name);
        //Debug.Log("接触されたオブジェクト：" + Collider.gameObject.name);
        if (Collider.gameObject.name == "Cylinder (1)(Clone)")
        {
            timerScript.PenaltyTime(5);
            //Debug.Log("接触したオブジェクト：" + gameObject.name);
            // ゲームオブジェクトにアタッチされているコライダーを取得
            if (TryGetComponent(out Collider collider))
            {
                // コライダーが存在する場合、それを削除
                Destroy(collider);
            }
        }
    }
}
