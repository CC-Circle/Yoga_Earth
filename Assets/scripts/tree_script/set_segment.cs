using UnityEngine;
using System.Collections;

public class set_segment : MonoBehaviour
{
    [SerializeField] float grow_speed = 0.2f;
    public static float grow_speed_pub;

    [SerializeField] bool is_key = false;
    public static bool is_key_pub;

    public GameObject cube_obj;
    float add_value = 0.29f;//間隔

    public static Vector3 top_position;//頂点格納

    private Vector3[] vertices;
    private Mesh mesh;

    GameObject obj;
    GameObject obj2;
    GameObject obj3;

    int branch_count = 0;

    nobiru_cube nobiru_class;//関連クラス

    float tree_start_x = 0;//初期位置
    float tree_start_y = 0;//初期位置
    float tree_start_z = 0;//初期位置

    void Start()
    {

        grow_speed_pub = grow_speed;
        is_key_pub = is_key;


        setTopPositionX(tree_start_x);
        setTopPositionY(tree_start_y);
        setTopPositionZ(tree_start_z);

        obj = Instantiate(cube_obj, top_position, Quaternion.identity);
        nobiru_class = obj.GetComponent<nobiru_cube>();

        // コルーチンを開始
        StartCoroutine(RepeatedCoroutine());
    }

    private void Update()
    {
        GetTopPosition(obj);
    }

    IEnumerator RepeatedCoroutine()
    {
        float y_value = 0;
        while (true)
        {
            if (nobiru_class.is_limit == false)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(0.05f);  // チャタリング

                y_value += add_value;
                setTopPositionX(getTopPositionx());
                setTopPositionY(tree_start_y + y_value);
                setTopPositionZ(tree_start_z);

                obj = Instantiate(cube_obj, top_position, Quaternion.identity);
                nobiru_class = obj.GetComponent<nobiru_cube>();
                branch_count++;
            }

        }
    }

    Vector3 GetTopPosition(GameObject in_obj)
    {
        Vector3 topPosition = Vector3.zero;
        float maxY = float.MinValue;

        mesh = in_obj.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // 最高点の頂点を探す
        foreach (var vertex in vertices)
        {
            if (vertex.y > maxY)
            {
                maxY = vertex.y;
                topPosition = in_obj.transform.TransformPoint(vertex); // ローカル座標からワールド座標に変換
            }
        }

        top_position = topPosition;
        top_position.x = topPosition.x + 0.475f;
        return topPosition;
    }

    public float getTopPositionx()
    {
        
        Vector3 topPosition = Vector3.zero;
        float maxY = float.MinValue;

        mesh = obj.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // 最高点の頂点を探す
        foreach (var vertex in vertices)
        {
            if (vertex.y > maxY)
            {
                maxY = vertex.y;
                topPosition = obj.transform.TransformPoint(vertex); // ローカル座標からワールド座標に変換
            }
        }

        topPosition.x = topPosition.x + 0.475f;

        return topPosition.x;
    }

    public float getTopPositiony()
    {


        Vector3 topPosition = Vector3.zero;
        float maxY = float.MinValue;

        mesh = obj.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // 最高点の頂点を探す
        foreach (var vertex in vertices)
        {
            if (vertex.y > maxY)
            {
                maxY = vertex.y;
                topPosition = obj.transform.TransformPoint(vertex); // ローカル座標からワールド座標に変換
            }
        }

        topPosition.x = topPosition.x + 0.475f;
        return topPosition.y;
    }

    public float getTopPositionz()
    {


        Vector3 topPosition = Vector3.zero;
        float maxY = float.MinValue;

        mesh = obj.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // 最高点の頂点を探す
        foreach (var vertex in vertices)
        {
            if (vertex.y > maxY)
            {
                maxY = vertex.y;
                topPosition = obj.transform.TransformPoint(vertex); // ローカル座標からワールド座標に変換
            }
        }

        topPosition.x = topPosition.x + 0.475f;
        return topPosition.z;
    }
    
    public void setTopPositionX(float topPositionX)
    {
        top_position.x = topPositionX;
    }

    public void setTopPositionY(float topPositionY)
    {
        top_position.y = topPositionY;
    }

    public void setTopPositionZ(float topPositionZ)
    {
        top_position.z = topPositionZ;
    }

}
