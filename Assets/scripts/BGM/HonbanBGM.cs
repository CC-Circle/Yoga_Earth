using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonbanBGM : MonoBehaviour
{
    private AudioSource audioSource;    // AudioSource コンポーネントを指定
    [SerializeField] AudioClip newBGM;           // 新しい BGM の AudioClip

    void Start()
    {
        audioSource = GetComponent<AudioSource>();  // AudioSource コンポーネントを取得
    }

    public void ChangeBGM()
    {
        if (audioSource == null || newBGM == null)
        {
            Debug.LogError("AudioSource または AudioClip が設定されていません。");
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();  // 現在の BGM を停止
        }

        audioSource.clip = newBGM;  // 新しい BGM に変更
        audioSource.Play();          // 新しい BGM を再生
    }
}
