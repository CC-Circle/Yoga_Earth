using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class Get_Score : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject save_score; // Save_Scoreオブジェクト

    [SerializeField] public TextMeshProUGUI score_text; // Textオブジェクト
    [SerializeField] public TextMeshProUGUI result; // Textオブジェクト

    int[] score;//スコアを格納する配列

    void Start()
    {
        save_score = GameObject.Find("Save_Score");
        score_text.enabled = false;
        result.enabled = false;
        // score_object = GameObject.Find("Text");
        // score = save_score.GetComponent<SaveScore>().GetTopScores();
        // Debug.Log("score");
    }

    // Update is called once per frame
    public void Score()
    {
        score_text.enabled = true;
        result.enabled = true;


        // Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        // canvas.enabled = true;

        score = save_score.GetComponent<SaveScore>().GetTopScores();
        // デバッグ用
        // Debug.Log("score");
        int count = 1;
        foreach (var score in score)
        {
            //Debug.Log(score);
            if (count == 1)
            {
                score_text.text = count + "st: " + score + "\n";
                count++;
                continue;
            }
            if (count == 2)
            {
                score_text.text += count + "nd: " + score + "\n";
                count++;
                continue;
            }
            if (count == 3)
            {
                score_text.text += count + "rd: " + score + "\n";
                count++;
                continue;
            }
            score_text.text += count + "th: " + score + "\n";
            count++;
        }
        if (count == 2)
        {
            score_text.text += 2 + "nd: " + 0 + "\n" +
            3 + "rd: " + 0 + "\n" + 4 + "th: " + 0 + "\n" + 5 + "th: " + 0 + "\n";
        }
        if (count == 3)
        {
            score_text.text += 3 + "rd: " + 0 + "\n" + 4 + "th: " + 0 + "\n" + 5 + "th: " + 0 + "\n";
        }
        if (count == 4)
        {
            score_text.text += 4 + "th: " + 0 + "\n" + 5 + "th: " + 0 + "\n";
        }
        if (count == 5)
        {
            score_text.text += 5 + "th: " + 0 + "\n";
        }
    }
}
