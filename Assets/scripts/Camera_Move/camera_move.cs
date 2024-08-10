using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;


public class camera_move : MonoBehaviour
{

    public void MoveCamera()
    {
        Vector3 position = set_segment.top_position + new Vector3(0, 6, 0);
        float speed = 1.0f;
        Camera camera = Camera.main;

        float l = (((position.y) / 2) / Mathf.Tan(camera.fieldOfView/2 * Mathf.PI / 180));
        Debug.Log("l: " + l);
        Debug.Log("position.y: " + position.y);

        Vector3 target = new Vector3(0, position.y/2, -l);

        Debug.Log("target: " + target);

        camera.transform.position = target;
        // camera.transform.position = Vector3.Lerp(camera.transform.position, target, speed * Time.deltaTime);
    }
}
