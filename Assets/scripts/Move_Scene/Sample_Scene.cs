using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float goal;
    private float top_position_y = 0.0f;

    [SerializeField] private GameObject tcp;

    [SerializeField] private GameObject camera;
    void Start()
    {
        //Debug.Log("Sample Scene");
        set_segment.top_position = Vector3.zero;
    }

    // Update is called once per frame
    async void Update()
    {
        top_position_y = set_segment.top_position.y;
        if (top_position_y >= goal)
        {
            //Debug.Log("Goal!");
            TCP tcpScript = tcp.GetComponent<TCP>();
            await tcpScript.DisconnectFromServerAsync();
            // UnityEngine.SceneManagement.SceneManager.LoadScene("End");
            Debug.Log("Goal!");
            camera_move cameraScript = camera.GetComponent<camera_move>();
            cameraScript.MoveCamera();
        }
    }
}
