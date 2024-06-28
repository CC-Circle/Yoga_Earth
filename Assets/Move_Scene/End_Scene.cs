using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("End Scene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            // Load the title scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
}
