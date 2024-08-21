using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //もし1キーが押されたら
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
        //もし2キーが押されたら
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }
        //もし3キーが押されたら
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Honban");
        }
    }
}
