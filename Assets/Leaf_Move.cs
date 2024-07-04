using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_Move : MonoBehaviour
{
    private GameObject leafs;

    void Start()
    {
        // このスクリプトがアタッチされているゲームオブジェクトをleafに代入
        leafs = this.gameObject;
        Debug.Log("Leafs initialized: " + leafs.name);
    }

    // Update is called once per frame
    void Update()
    {
        // set_segment クラスが正しく定義されていることを確認してください
        // ここでは set_segment.top_position を取得します
        float top_position_x = set_segment.top_position.x * 0.6f - 1.2f;
        if (top_position_x > 0.8f)
        {
            top_position_x = 0.8f;
        }
        if (top_position_x < -3.0f)
        {
            top_position_x = -3.0f;
        }
        float top_position_y = set_segment.top_position.y + 5.0f;
        Debug.Log("Top position - X: " + top_position_x + ", Y: " + top_position_y);

        // leafsのx座標とy座標をtop_positionに合わせて更新
        Vector3 newPosition = leafs.transform.position;
        newPosition.x = top_position_x;
        newPosition.y = top_position_y;
        leafs.transform.position = newPosition;

        Debug.Log("Leafs new position: " + leafs.transform.position);
    }
}
