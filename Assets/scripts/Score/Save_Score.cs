using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    private static readonly List<int> scores = new();
    [SerializeField] private int maxTopScores = 5;  // 保存される上位スコアの最大数
    //[SerializeField] private TextMeshProUGUI score_text;
    [SerializeField] private TextMeshProUGUI ranking_text;

    //private int[] score;

    void Start()
    {
        //SaveNewScore((int)set_segment.top_position.y);
        //score_text.text = "";
        ranking_text.text = "";
    }

    public void SaveNewScore(int newScore)
    {
        //DisplayScores(newScore);
        // 新しいスコアをリストに追加
        if (newScore == 0)
        {
            return;
        }
        scores.Add(newScore);

        // 降順にソート
        scores.Sort((a, b) => b.CompareTo(a));

        // 上位5つのスコアのみを保持
        if (scores.Count > maxTopScores)
        {
            scores.RemoveAt(scores.Count - 1); // 最後の要素（maxTopScores+1番目以降）を削除
        }

        //int[] score = scores.ToArray();
        DisplayLanking(scores);

        // デバッグ用
        //print_scores();
    }

    //void DisplayScores(int Score)
    //{
    //    score_text.text = "Score: " + Score;
    //}

    void DisplayLanking(List<int> scores)
    {
        int count = 1;
        int totalRanks = 5; // 表示する最大順位

        ranking_text.text = "";

        foreach (var score in scores)
        {
            ranking_text.text += GetRankSuffix(count) + ": " + score + "\n";
            count++;
        }

        // 残りの順位に0を表示する
        for (int i = count; i <= totalRanks; i++)
        {
            ranking_text.text += GetRankSuffix(i) + ": 0\n";
        }
    }

    string GetRankSuffix(int rank)
    {
        if (rank == 1) return rank + "st";
        if (rank == 2) return rank + "nd";
        if (rank == 3) return rank + "rd";
        return rank + "th";
    }


    // 上位スコアを取得するメソッド（例）
    public int[] GetTopScores()
    {
        return scores.ToArray(); // 配列として返す
    }
}
