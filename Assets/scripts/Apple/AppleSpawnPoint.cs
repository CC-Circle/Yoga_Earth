using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawnPoint : MonoBehaviour
{
    private bool isApple = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetIsApple()
    {
        return isApple;
    }

    public void SetIsApple(bool isApple)
    {
        this.isApple = isApple;
    }
}
