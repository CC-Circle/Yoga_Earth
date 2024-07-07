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
    public GameObject save_score;

    [SerializeField] public TextMeshProUGUI score_text; // Textオブジェクト

    int[] score;

    void Start()
    {
        save_score =  GameObject.Find("Save_Score");
        // score_object = GameObject.Find("Text");
        score = save_score.GetComponent<SaveScore>().GetTopScores();
        // Debug.Log("score");
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用
        // Debug.Log("score");
        // for (int i = 0; i < 5; i++)
        // {
        //     Array.Resize(ref score, i+1);
        //     score[i] = i+1;
        //     Debug.Log(i + 1 + "th: " + score[i]);
        // }
        // テキストの表示を入れ替える
        score_text.text = "1st: " + score[0] + "\n" +
                          "2nd: " + score[1] + "\n" +
                          "3rd: " + score[2] + "\n" +
                          "4th: " + score[3] + "\n" +
                          "5th: " + score[4] + "\n";
    }
}
