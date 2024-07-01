using System.Collections.Generic;
using UnityEngine;

public class ObjectColliderManager : MonoBehaviour
{
    private List<GameObject> createdObjects = new List<GameObject>();
    
    void Update()
    {
        // 名前を使って特定のオブジェクトを取得
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            if (obj.name == "Cylinder (1)(Clone)" && !createdObjects.Contains(obj))
            {
                createdObjects.Add(obj);
                // Debug.Log("Added object: " + obj.name); // デバッグ用
            }
        }
        
        UpdateColliders();
        DebugObjectsList(); // デバッグ用
    }

    void UpdateColliders()
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            GameObject obj = createdObjects[i];
            Collider collider = obj.GetComponent<Collider>();

            if (i == createdObjects.Count - 1)
            {
                // 最新のオブジェクトにコライダーを追加
                if (collider == null)
                {
                    obj.AddComponent<BoxCollider>(); // 必要に応じて他のコライダーに変更
                }
            }
            else
            {
                // それ以外のオブジェクトからコライダーを削除
                if (collider != null)
                {
                    Destroy(collider);
                }
            }
        }
    }

    void DebugObjectsList()
    {
        Debug.Log("Objects in list: " + createdObjects.Count); // リストの数を表示
        for (int i = 0; i < createdObjects.Count; i++)
        {
            // Debug.Log("Object " + i + ": " + createdObjects[i].name); // 各オブジェクトの名前を表示
        }
    }
}
