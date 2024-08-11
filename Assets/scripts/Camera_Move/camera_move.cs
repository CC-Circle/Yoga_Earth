using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;


public class camera_move : MonoBehaviour
{


    [SerializeField] private float speed;  // 移動のスピード

    private Transform cameraTransform;
    private Vector3 target;
    private bool isMoving = false;

    private Camera camera;


    // void Update()
    // {
    //     Debug.Log("isMoving: " + isMoving);
    //     if (isMoving)
    //     {
    //         // カメラの現在位置をtargetPositionの位置へ向かって滑らかに移動させる
    //         camera.transform.position = Vector3.Lerp(camera.transform.position, target, speed * Time.deltaTime);
    //         Debug.Log("camera.transform.position: " + camera.transform.position);
            
    //         // 目標位置に近づいた場合、移動を停止する
    //         if (Vector3.Distance(camera.transform.position, target) < 0.01f)
    //         {
    //             camera.transform.position = target; // 正確な位置に設定
    //             isMoving = false;
    //         }
    //     }
    // }


    public void MoveCamera()
    {
        Vector3 position = set_segment.top_position;
        // float speed = 1.0f;
        camera = Camera.main;

        float l = (((position.y+6) / 2) / Mathf.Tan(camera.fieldOfView/2 * Mathf.PI / 180));
        Debug.Log("l: " + l);
        Debug.Log("position.y: " + position.y);

        target = new Vector3(position.y, position.y/2, -l);

        Debug.Log("target: " + target);

        // camera.transform.position = target;
        // camera.transform.position = Vector3.Lerp(camera.transform.position, target, speed * Time.deltaTime);

        while (camera.transform.position != target)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, target, speed * Time.deltaTime);
            Debug.Log("camera.transform.position: " + camera.transform.position);
            
            // 目標位置に近づいた場合、移動を停止する
            if (Vector3.Distance(camera.transform.position, target) < 0.01f)
            {
                camera.transform.position = target; // 正確な位置に設定
                break;
            }
        }
    }

    public void StartMove()
    {
        Debug.Log("StartMove");
        isMoving = true;
    }
}
