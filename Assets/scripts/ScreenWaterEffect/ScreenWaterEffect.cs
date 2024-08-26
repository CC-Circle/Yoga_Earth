using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWaterEffect : MonoBehaviour
{
    [Range(5, 64)]
    [SerializeField] private float Distortion = 8.0f;
    [Range(0, 7)]
    [SerializeField] private float SizeX = 1f;
    [Range(0, 7)]
    [SerializeField] private float SizeY = 0.5f;
    [Range(0, 10)]
    [SerializeField] private float DropSpeed = 3.6f;

    [SerializeField] private Texture WaterTexture;
    [SerializeField] private Shader curShader;
    private Material curMaterial;
    private float TimeX = 0f;

    public static bool isEffectActive = false; // エフェクトのオン/オフ状態を管理

    private AudioSource audioSource;
    private AudioClip waterDropSE;

    // Materialプロパティ
    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }

    void Start()
    {
        isEffectActive = false;
        audioSource = GetComponent<AudioSource>();
    }

    // 無効化時の処理
    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }

    void Update()
    {
        // スペースキーが押されたらエフェクトを切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isEffectActive = !isEffectActive;
        }
    }

    // レンダリング後の画像処理
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (isEffectActive && curShader != null)
        {
            TimeX += Time.deltaTime;
            if (TimeX >= 100)
                TimeX = 0;

            material.SetFloat("_CurTime", TimeX);
            material.SetFloat("_Distortion", Distortion);
            material.SetFloat("_SizeX", SizeX);
            material.SetFloat("_SizeY", SizeY);
            material.SetFloat("_DropSpeed", DropSpeed);
            material.SetTexture("_ScreenWaterDropTex", WaterTexture);

            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public IEnumerator SetEffectActive()
    {
        isEffectActive = true;
        audioSource.Play();

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Distortion += 0.7f;
            audioSource.volume -= 0.01f;
        }
        isEffectActive = false;
        Distortion = 8.0f;
        audioSource.volume = 1.0f;
        audioSource.Stop();
    }
}
