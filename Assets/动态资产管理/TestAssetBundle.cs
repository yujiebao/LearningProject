using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TestAssetBundle : MonoBehaviour
{
    private  string bundlePath ;  

    private void Start() {
        //  bundlePath = GetRootPath()+"/"+"/AssetBundles/Windows";
        // print(Application.streamingAssetsPath);
        // //加载assets目录下的ab包
        // AssetBundle myAb = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+"myabpacket");
        // //从ab包获取资 源
        // GameObject sphere = myAb.LoadAsset<GameObject>("Sphere");
        // Instantiate(sphere);


        //加载压缩的ab包
        // string bundlePath = GetRootPath()+"/"+"/AssetBundles/Windows";
        // AssetBundle myAb = AssetBundle.LoadFromFile(bundlePath+"/"+"myabpacket");
        // GameObject sphere = myAb.LoadAsset<GameObject>("Sphere");
        // Instantiate(sphere);
        // GameObject cube = myAb.LoadAsset<GameObject>("Cube");
        // Instantiate(cube);


        //加载依赖的例子
        // string bundlePath = GetRootPath()+"/"+"/AssetBundles/Windows";  
        // AssetBundle myMaterial = AssetBundle.LoadFromFile(bundlePath+"/"+"material");  //加载依赖的材质
        // AssetBundle myAb = AssetBundle.LoadFromFile(bundlePath+"/"+"myabpacket");
        // GameObject sphere = myAb.LoadAsset<GameObject>("Sphere");
        // Instantiate(sphere);


        //通过主包加载依赖
        // string bundlePath = GetRootPath()+"/"+"/AssetBundles/Windows";  已经改为类外声明全类可用  
        // AssetBundle mainBundle = AssetBundle.LoadFromFile(bundlePath+"/"+"Windows");   //Windows保存所有保的依赖  mac为OSX
        // AssetBundleManifest manifest= mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        // string[] dependencies = manifest.GetAllDependencies("myabpacket");    //当前包的所有依赖的包
        // foreach(string dependency in dependencies)
        // {
        //     AssetBundle.LoadFromFile(bundlePath+"/"+dependency);
        // }
        // AssetBundle myAb = AssetBundle.LoadFromFile(bundlePath+"/"+"myabpacket");
        // GameObject sphere = myAb.LoadAsset<GameObject>("Sphere");
        // Instantiate(sphere);
        // StartCoroutine(LoadAssetAsync());
        StartCoroutine(loadAssetFromWeb());
    }
    IEnumerator loadAssetFromWeb()
    {
        // AssetBundle mainBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+"Windows");
        // AssetBundleManifest manifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        // string[] dependencies = manifest.GetAllDependencies("myabpacket");
        // foreach (string dependency in dependencies)    
        // {
        //     AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+dependency);
        // }  //从asset中读取依赖


        // AssetBundle mainBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+"Windows");
        // AssetBundleManifest manifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        // string[] dependencies = manifest.GetAllDependencies("myabpacket");//从asset中读主包获取依赖包名，然后在网络上加载依赖包
        // foreach (string dependency in dependencies)
        // {
        //     UnityWebRequest request1 = UnityWebRequestAssetBundle.GetAssetBundle("http://localhost:8080/Windows"+"/"+"dependency");
        //     yield return request1.SendWebRequest();
        //     AssetBundle bundle1 = DownloadHandlerAssetBundle.GetContent(request1); //bundle就是ab包可以获取ab包中的内容
        // }
    //    ！！！-----有问题异步操作不适合写在for里面，for执行较快

        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle("http://localhost:8080/Windows/myabpacket");
        yield return request.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request); //bundle就是ab包可以获取ab包中的内容
        GameObject Sphere = bundle.LoadAsset<GameObject>("Sphere");
        Instantiate(Sphere);
    }
        //异步加载
        // IEnumerator LoadAssetAsync()
        // {
        //     //1.异步加载ab包
        //     AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundlePath+"/"+"myabpacket");   //ab包
        //     yield return request;
        //     AssetBundleCreateRequest request3 = AssetBundle.LoadFromFileAsync(bundlePath+"/"+"material");
        //     yield return request3;

        //     //2.异步加载资源
        //     AssetBundle myAb = request.assetBundle;   //挂起当前的协程，直到 request 完成
        //     AssetBundleRequest request2 = myAb.LoadAssetAsync<GameObject>("Sphere");
        //     yield return request2;  //挂起当前的协程，直到 request2 完成

        //     GameObject sphere = request2.asset as GameObject;
        //     Instantiate(sphere);
        // }

    // private string GetRootPath()
    // {
    //     return Directory.GetParent(Application.dataPath).FullName;
    // }
}
