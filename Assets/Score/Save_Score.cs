using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    private static readonly List<int> scores = new();
    [SerializeField] private int maxTopScores;  // 保存される上位スコアの最大数

    public void SaveNewScore(int newScore)
    {
        // 新しいスコアをリストに追加
        scores.Add(newScore);

        // 降順にソート
        scores.Sort((a, b) => b.CompareTo(a));

        // 上位5つのスコアのみを保持
        if (scores.Count > maxTopScores)
        {
            scores.RemoveAt(scores.Count - 1); // 最後の要素（maxTopScores+1番目以降）を削除
        }

        // デバッグ用
        //print_scores();
    }

    // 上位スコアを取得するメソッド（例）
    public int[] GetTopScores()
    {
        return scores.ToArray(); // 配列として返す
    }

    // デバッグ用
    private void print_scores()
    {
        foreach (var score in scores)
        {
            Debug.Log(score);
        }
    }
}
