using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTreeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject appleTreePrefab;
    [SerializeField] private GameObject appleTreePrefab2;
    [SerializeField] private GameObject appleTreePrefab3;
    [SerializeField] private GameObject appleTreePrefab4;

    [SerializeField] private GameObject appleTreeArea1;
    [SerializeField] private GameObject appleTreeArea2;

    [SerializeField] private GameObject terrainObj;
    private Terrain terrain;

    private List<GameObject> appleTreePrefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        terrain = terrainObj.GetComponent<Terrain>();
        appleTreePrefabs.Add(appleTreePrefab);
        appleTreePrefabs.Add(appleTreePrefab2);
        appleTreePrefabs.Add(appleTreePrefab3);
        appleTreePrefabs.Add(appleTreePrefab4);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool AppleTreeAreaCheck(Vector3 appleTreePosition)
    {
        // appleTreePositionがappleTreeArea1とappleTreeArea2の範囲内にあるかどうかを判定
        if (appleTreeArea1.transform.position.x <= appleTreePosition.x && appleTreePosition.x <= appleTreeArea1.transform.position.x + appleTreeArea1.transform.localScale.x &&
            appleTreeArea1.transform.position.z <= appleTreePosition.z && appleTreePosition.z <= appleTreeArea1.transform.position.z + appleTreeArea1.transform.localScale.z)
        {
            return true;
        }
        return false;
    }

    public void CreateAppleTree(int treeNum)
    {
        //Debug.Log("CreateAppleTree");
        for (int i = 0; i < treeNum; i++)
        {
            // AppleTreeの生成位置をランダムに決定
            Vector3 appleTreePosition = new Vector3(Random.Range(appleTreeArea1.transform.position.x, appleTreeArea2.transform.position.x), 0.0f, Random.Range(appleTreeArea1.transform.position.z, appleTreeArea2.transform.position.z));

            // AppleTreeの生成位置の高さを取得
            appleTreePosition.y = GetHeightAtPosition(appleTreePosition.x, appleTreePosition.z);

            // AppleTreeの生成位置がappleTreeArea1とappleTreeArea2の範囲内にあるかどうかを判定
            //if (AppleTreeAreaCheck(appleTreePosition))
            //{
            // AppleTreeを生成
            int randomNum = Random.Range(0, 3);
            GameObject randomAppleTreePrefab = appleTreePrefabs[randomNum];
            GameObject appleTreeInstance = Instantiate(randomAppleTreePrefab, appleTreePosition, Quaternion.identity);
            //Debug.Log("AppleTree is created at " + appleTreePosition);
            //}
            /*
            else
            {
                Debug.Log("AppleTree is not created at " + appleTreePosition);
            }
            */
        }
    }

    private float GetHeightAtPosition(float x, float z)
    {
        // 高さを取得
        float height = terrain.SampleHeight(new Vector3(x, 0, z));
        return height;
    }
}
