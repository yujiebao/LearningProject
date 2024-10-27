  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于测试AssetBundleManager
/// </summary> 
public class TestABManager : MonoBehaviour
{
     string bundleBasePath = Application.streamingAssetsPath;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject sphere;
        AssetBundleManager.Instance.Init("http://localhost:8080");
        // GameObject sphrere = AssetBundleManager.Instance.LoadAsset<GameObject>("myabpacket","Sphere");
        // Instantiate(sphrere);

        AssetBundleManager.Instance.LoadAssetAsync<GameObject>("myabpacket","Sphere",(obj)=>{
          // sphere = obj as GameObject;
          Instantiate(obj);
           AssetBundleManager.Instance.LoadAssetAsync<GameObject>("cube","Cube",(obj)=>{
          // cube = obj as GameObject;
          Instantiate(obj);
        });
        });

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
