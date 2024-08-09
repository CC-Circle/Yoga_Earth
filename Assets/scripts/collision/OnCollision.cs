using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("作動中");
    }
    void OnTriggerEnter(Collider Collider)
    {
        // Debug.Log("接触したオブジェクト：" + gameObject.name);
        // Debug.Log("接触されたオブジェクト：" + Collider.gameObject.name);
        UnityEngine.SceneManagement.SceneManager.LoadScene("End");

    }
}
