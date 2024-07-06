using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // 目標のゲームオブジェクト
    // public Transform target;
    private float top_position_y = 0.0f;

    // 移動速度
    [SerializeField] private float moveSpeed;

    // 距離のしきい値
    [SerializeField] private float distanceThreshold;

    void Start()
    {
        set_segment.top_position = Vector3.zero;

    }

    void Update()
    {
        top_position_y = set_segment.top_position.y;
        // targetのY座標との差を計算
        float yDistance = Mathf.Abs(top_position_y - transform.position.y);

        // Y座標の差がしきい値以下の場合
        if (yDistance < distanceThreshold)
        {
            // 黄緑色の矢印の方向に移動させる
            Vector3 upwardDirection = transform.up;

            // 移動させる
            transform.position += upwardDirection * moveSpeed * Time.deltaTime;
        }
    }
}
