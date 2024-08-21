using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI derectionText;
    [SerializeField] private GameObject TutorialObstacleSpawner;

    [SerializeField] private UnityEngine.UI.Image rightGoal;
    [SerializeField] private UnityEngine.UI.Image leftGoal;

    private bool firstProgressStarted = false;
    private bool firstProgressFinished = false;
    private bool secondProgressStarted = false;
    private bool secondProgressFinished = false;
    private bool thirdProgressStarted = false;
    private bool thirdProgressFinished = false;

    private bool isReachedRight = false;
    private bool isReachedLeft = false;

    private FloatingObstacleSpawner floatingObstacleSpawner;



    // Start is called before the first frame update
    void Start()
    {
        Timer.isTimeUp = false;

        floatingObstacleSpawner = TutorialObstacleSpawner.GetComponent<FloatingObstacleSpawner>();

        rightGoal.enabled = false;
        leftGoal.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstProgressStarted == false)
        {
            firstProgressStarted = true;
            derectionText.text = "Pose as a tree and stretch the tree on the screen!";
            Debug.Log("Starting FirstProgress coroutine.");
            StartCoroutine(FirstProgress());
        }

        if (firstProgressFinished == true && secondProgressFinished == false)
        {
            rightGoal.enabled = true;
            leftGoal.enabled = true;
            derectionText.text = "Tilt your body to tilt the tree left or right in the screen!";
            timerText.text = "";
            //Debug.Log(set_segment.top_position.x);
            if (set_segment.top_position.x > 2.5f)
            {
                isReachedRight = true;
                //rightGoalの色を緑に変える
                rightGoal.color = new Color(0, 1, 0, 0.27f);

                //Debug.Log("Reached Right");
            }
            else if (set_segment.top_position.x < -1.5f)
            {
                isReachedLeft = true;
                //leftGoalの色を緑に変える
                leftGoal.color = new Color(0, 1, 0, 0.27f);

                //Debug.Log("Reached Left");
            }

            if (isReachedLeft == true && isReachedRight == true)
            {
                secondProgressFinished = true;
            }
        }

        if (secondProgressFinished == true && thirdProgressStarted == false)
        {
            thirdProgressStarted = true;
            derectionText.text = "Let's extend the tree while avoiding obstacles!";
            StartCoroutine(ThirdProgress());
            StartCoroutine(floatingObstacleSpawner.ExecuteAtRandomIntervals());
        }
        if (thirdProgressFinished == true)
        {
            //チュートリアル終了
            Debug.Log("Tutorial Finished");
            timerText.text = "";
            derectionText.text = "Tutorial Finished!";
            StartCoroutine(ToTitleScene());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //チュートリアルをスキップして本番へ
            UnityEngine.SceneManagement.SceneManager.LoadScene("Honban");
        }


    }

    IEnumerator ToTitleScene()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Honban");
    }

    IEnumerator FirstProgress()
    {
        int currentTime = 0;
        int timeLimit = 10;
        // currentTime が timeLimit に達するまでループ
        while (currentTime < timeLimit)
        {
            timerText.text = "Limit: " + (timeLimit - currentTime);
            // 1秒待機
            yield return new WaitForSeconds(1);
            // 時間をカウント
            currentTime++;
            //Debug.Log("Time: " + currentTime);
        }
        timerText.text = "Limit: 0";
        firstProgressFinished = true;
        yield return null;
    }

    IEnumerator SecondProgress()
    {
        yield return new WaitForSeconds(2);
        bool isReachedRight = false;
        bool isReachedLeft = false;
        timerText.text = "";

        while (true)
        {
            if (set_segment.top_position.x > 4.5f)
            {
                isReachedRight = true;
            }
            if (set_segment.top_position.x < -4.5f)
            {
                isReachedLeft = true;
            }
            if (isReachedLeft == true && isReachedRight == true)
            {
                secondProgressFinished = true;
                break;
            }
        }
        yield return null;
    }

    IEnumerator ThirdProgress()
    {
        int currentTime = 0;
        int timeLimit = 25;
        while (currentTime < timeLimit)
        {
            timerText.text = "Limit: " + (timeLimit - currentTime);
            // 1秒待機
            yield return new WaitForSeconds(1);

            rightGoal.enabled = false;
            leftGoal.enabled = false;

            // 時間をカウント
            currentTime++;
            //Debug.Log("Time: " + currentTime);
        }
        thirdProgressFinished = true;
        yield return null;
    }
}
