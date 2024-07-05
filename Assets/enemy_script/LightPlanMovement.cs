using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlanMovement : MonoBehaviour
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
            // 目標のZ座標を計算
            float targetZ = transform.position.z - 300;

            // 現在の位置から目標位置への方向を計算
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, targetZ);
            Vector3 direction = (targetPosition - transform.position).normalized;

            // 目標位置に向けて移動
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
