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
        float top_position_x = set_segment.top_position.x + 0.3f;
        if (top_position_x > 5.5f)
        {
            top_position_x = 5.5f;
        }
        if (top_position_x < -4.5f)
        {
            top_position_x = -4.5f;
        }
        float top_position_y = set_segment.top_position.y + 1.5f;
        //Debug.Log("Top position - X: " + top_position_x + ", Y: " + top_position_y);

        // leafsのx座標とy座標をtop_positionに合わせて更新
        Vector3 newPosition = leafs.transform.position;
        newPosition.x = top_position_x;
        newPosition.y = top_position_y;
        leafs.transform.position = newPosition;

        //Debug.Log("Leafs new position: " + leafs.transform.position);
    }
}