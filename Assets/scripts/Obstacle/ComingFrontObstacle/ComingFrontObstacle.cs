using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingFrontObstacle : MonoBehaviour
{
    // メインカメラの外にいると判断されるまでの時間（秒）
    [SerializeField] private float timeOutsideViewThreshold = 1.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distanceThreshold = 0.1f;

    // メインカメラの外にいる時間をカウントするための変数
    private float timeOutsideView = 0.0f;

    // ゲームオブジェクトが視界外にいるかどうかを追跡するフラグ
    private bool isOutsideView = false;

    private Vector3 targetPosition;

    private GameObject TimerObj;
    Timer timerScript;


    void Start()
    {
        //Debug.Log("ComingFrontObstacle script is attached to " + gameObject.name);
        float x = Random.Range(-5.0f, 5.0f);
        SetTargetPosition(new Vector3(x, set_segment.top_position.y + 22.0f, -10.0f));
        TimerObj = GameObject.Find("Timer");
        timerScript = TimerObj.GetComponent<Timer>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > distanceThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = targetPosition;
        }

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

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private Vector3 GetTargetPosition()
    {
        //Debug.Log("Target Position: " + targetPosition);
        return targetPosition;
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
