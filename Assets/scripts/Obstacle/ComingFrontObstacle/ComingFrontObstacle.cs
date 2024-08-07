using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingFrontObstacle : MonoBehaviour
{
    // メインカメラの外にいると判断されるまでの時間（秒）
    [SerializeField] private float timeOutsideViewThreshold = 3.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distanceThreshold = 0.1f;

    // メインカメラの外にいる時間をカウントするための変数
    private float timeOutsideView = 0.0f;

    // ゲームオブジェクトが視界外にいるかどうかを追跡するフラグ
    private bool isOutsideView = false;

    private Vector3 targetPosition;


    void Start()
    {
        //Debug.Log("ComingFrontObstacle script is attached to " + gameObject.name);
        SetTargetPosition(new Vector3(2.0f, set_segment.top_position.y + 24.0f, -8.0f));
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
        Debug.Log("接触したオブジェクト：" + gameObject.name);
        //Debug.Log("接触されたオブジェクト：" + Collider.gameObject.name);

        // ゲームオブジェクトにアタッチされているコライダーを取得
        if (TryGetComponent(out Collider collider))
        {
            // コライダーが存在する場合、それを削除
            Destroy(collider);
        }
    }
}
