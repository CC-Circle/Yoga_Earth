using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rain_cloud_move : MonoBehaviour
{
    public float waittime = 7f;//雨が降り出すまでの時間

    public float durasion_rain = 10f;//雨が降り続ける時間

    public float dissolve_start_time = 0f;//雨が降り終わってから消えるまでの時間

    public float effectDuration = 3f;//雲が消える時間（値が大きいほどゆっくり消える）

    public GameObject rain_pa;
    public GameObject rain_coll;


    [SerializeField]
    Renderer[] renderers = { };


    Ease effectEase = Ease.Linear;
    [SerializeField]
    string progressParamName = "_Progress";

    List<Material> materials = new List<Material>();
    Sequence sequence;
    BoxCollider Box;

    private GameObject TimerObj;
    Timer timerScript;

    private bool isHitTree = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAtRandomIntervals());
        GetMaterials();
        Box = GetComponent<BoxCollider>();
        Box.enabled = false;
        //rain_pa.Stop();

        TimerObj = GameObject.Find("Timer");
        timerScript = TimerObj.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - set_segment.top_position.y < 3.5)
        {
            transform.position = new Vector3(transform.position.x, set_segment.top_position.y + 3.5f, 0);


        }
        if (Timer.isTimeUp)
        {
            Destroy(gameObject);
        }


    }

    IEnumerator ExecuteAtRandomIntervals()
    {

        // 一定の間隔を待つ
        yield return new WaitForSeconds(waittime);
        rain_pa.SetActive(true);
        Box.enabled = true;

        yield return new WaitForSeconds(durasion_rain);
        rain_pa.SetActive(false);
        Box.enabled = false;
        yield return new WaitForSeconds(dissolve_start_time);
        DissolveOut();

        yield return new WaitForSeconds(effectDuration);
        Destroy(this.gameObject);

    }

    public void DissolveOut()
    {
        sequence = DOTween.Sequence().SetLink(gameObject).SetEase(effectEase);

        foreach (Material m in materials)
        {
            m.SetFloat(progressParamName, 1);
            sequence.Join(m.DOFloat(0, progressParamName, effectDuration));
        }

        sequence.Play();
        //Debug.Log("ok");
    }

    void GetMaterials()
    {
        foreach (Renderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                materials.Add(m);
            }
        }
    }

    public void DissolveIn()
    {
        sequence = DOTween.Sequence().SetLink(gameObject).SetEase(effectEase);

        foreach (Material m in materials)
        {
            m.SetFloat(progressParamName, 0);
            sequence.Join(m.DOFloat(1, progressParamName, effectDuration));
        }

        sequence.Play();
    }



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("tree"))
        {
            if (!isHitTree)
            {
                timerScript.PenaltyTime(10);
                Debug.Log($"Hittag{other.gameObject.tag}");
            }
            isHitTree = true;
        }
    }
}
