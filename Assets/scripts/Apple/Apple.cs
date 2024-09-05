using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Apple : MonoBehaviour
{

    private bool isGrownApple = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator ScaleOverTime(Vector3 targetScale, float time)
    {
        Vector3 startScale = transform.localScale; // 初期スケール
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            // 線形補間 (Lerp) でサイズを変化させる
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / time);

            // 経過時間を更新
            elapsedTime += Time.deltaTime;

            // 目標サイズに到達したか確認
            if (elapsedTime >= time)
            {
                transform.localScale = targetScale; // 目標サイズに正確に設定
                isGrownApple = true;
                yield break; // コルーチンを終了
            }

            yield return new WaitForFixedUpdate();
        }

        // 最終的に目標サイズに到達
        transform.localScale = targetScale;
        isGrownApple = true;
    }

    public IEnumerator WaitDeleteApple(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public bool GetIsGrownApple()
    {
        return isGrownApple;
    }
}