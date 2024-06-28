using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foliage_scale : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(7f, 2f, 7f); // 最終的なスケール
    public float duration = 10f; // 拡大するまでの時間（秒）

    private Vector3 initialScale; // 初期スケール
    private float elapsedTime = 0f; // 経過時間

    void Start()
    {
        // 初期スケールを保存
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        // スケールの割合を計算
        float t = Mathf.Clamp01(elapsedTime / duration);

        // 線形補間でスケールを更新
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
    }
}
