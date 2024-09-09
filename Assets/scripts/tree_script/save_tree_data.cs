using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_tree_data : MonoBehaviour
{
    public static List<List<Vector3>> list_vertis = new List<List<Vector3>>();
    public static List<Vector3> list_zahyo = new List<Vector3>();

    [SerializeField] GameObject cube_obj;
    [SerializeField] GameObject top_glass;
    public Transform parentTran;

    [SerializeField] float moveSpeed = 100.0f; // メッシュの移動速度
    [SerializeField] float growthSpeed = 2.0f; // 1秒あたりの成長量
    [SerializeField] float growthLimit = 1.0f; // 最大成長高さ
    [SerializeField] float growthAngle = 30.0f; // 成長の角度 (X軸正方向からの傾き角度)
    [SerializeField] float left_limit = -2.0f;
    [SerializeField] float right_limit = 2.0f;
    [SerializeField] bool is_key = false;

    private static float growthDirection = 0.0f; // X軸方向の成長方向 (0: 真上, 1: 正方向, -1: 負方向)
    int tree_count = 0;

    int[] offset_Arrays = { 1, 10, 20, 50 , -30, 6 , -20, 40, -15, -10};

    void Start()
    {
        tree_count = 0;

        if (check_seg_count())
        {
            save_set_segm();
        }

        foreach (var item in list_vertis)
        {
            bool _ins_top = false;


            for (int i = 0; i < item.Count; i++)
            {
                GameObject obj = Instantiate(cube_obj, new Vector3(item[i].x + offset_Arrays[tree_count], item[i].y - 40f, item[i].z + 40f), Quaternion.identity);
                obj.transform.SetParent(parentTran);
                if (_ins_top == false)
                {
                    GameObject obj_glass = Instantiate(top_glass, new Vector3(item[item.Count - 1].x + offset_Arrays[tree_count], item[item.Count - 1].y - 40f, item[item.Count - 1].z + 40f), Quaternion.identity);
                    _ins_top = true;

                    obj_glass.transform.SetParent(parentTran);
                }
            }
            tree_count++;
        }
    }

    public static void save_segm()
    {
        list_zahyo.Add(set_segment.top_position);
    }

    public static void save_set_segm()
    {
        list_vertis.Add(new List<Vector3>(list_zahyo)); // コピーを追加
        if(list_vertis.Count > 10)
        {
            int cl_index = 0;
            int cl_count = 0;
            float min_y = 0;
            foreach (var item in list_vertis)
            {
                if(min_y < item[item.Count-1].y)
                {
                    min_y = item[item.Count - 1].y;
                    cl_index = cl_count;
                }
                cl_count++;
            }

            list_vertis.RemoveAt(cl_index);
        }
        list_zahyo.Clear(); // リストをクリア
    }

    public bool check_seg_count()
    {
        return list_zahyo.Count > 0;
    }
}
