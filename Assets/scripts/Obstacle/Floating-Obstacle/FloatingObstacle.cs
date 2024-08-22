using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingObstacle : MonoBehaviour
{
    // メインカメラの外にいると判断されるまでの時間（秒）
    [SerializeField] private float timeOutsideViewThreshold = 3.0f;

    // メインカメラの外にいる時間をカウントするための変数
    private float timeOutsideView = 0.0f;

    // ゲームオブジェクトが視界外にいるかどうかを追跡するフラグ
    private bool isOutsideView = false;

    //[SerializeField] private GameObject floatingObstacleObj;
    private GameObject TimerObj;
    Timer timerScript;


    void Start()
    {
        //Debug.Log("FloatingObstacle script is attached to " + gameObject.name);
        //Debug.Log("Hello, World!");
        //timerScript = floatingObstacleObj.GetComponent<Timer>();

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
