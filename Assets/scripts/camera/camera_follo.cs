using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follo : MonoBehaviour
{


    float count_x;

    public set_segment _SET_SEGMENT;

    // Start is called before the first frame update
    void Start()
    {
        //count_x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {


        //if (Input.GetKey(KeyCode.Space))
        //{
            transform.position = new Vector3(0f, _SET_SEGMENT.getTopPositiony() + 1.5f, _SET_SEGMENT.getTopPositionz() - 5f);
        //}
    }
}
