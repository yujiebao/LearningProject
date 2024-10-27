using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestResources : MonoBehaviour
{
    // public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(prefab);   //直接引用prefab
       Object cube =  Resources.Load("Cube");   //加载的不一定是游戏对象，所以不是GameObject
       Instantiate(cube);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
