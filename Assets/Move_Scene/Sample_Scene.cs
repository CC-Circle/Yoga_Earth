using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float goal;
    public SaveScore saveScoreScript;
    private float top_position_y = 0.0f;
    void Start()
    {
        //Debug.Log("Sample Scene");
        set_segment.top_position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        top_position_y = set_segment.top_position.y;
        if (top_position_y >= goal)
        {
            //Debug.Log("Goal!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("End");

            // スコアを保存
            saveScoreScript.SaveNewScore((int)top_position_y);
        }
    }
}
