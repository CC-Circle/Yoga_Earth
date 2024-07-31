using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    private int pose_cnt = 0;
    [SerializeField] private int GameStart;
    void Start()
    {
        Debug.Log("Title Scene");
        pose_cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            // Load the game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

        if (Receive_Data.x_zahyo != -1 && pose_cnt != 50)
        {
            pose_cnt++;
        }

        if (pose_cnt > GameStart)
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

        if (Receive_Data.x_zahyo == -1)
        {
            pose_cnt = -500;
        }
    }
}
