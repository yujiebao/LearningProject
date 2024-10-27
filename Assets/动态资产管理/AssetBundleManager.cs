using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
public class AssetBundleManager : MonoBehaviour
{
    public string bundleBasePath;
    private AssetBundleManifest manifest;
    
    // AssetBundle assetBundle;   改用缓存此处用字典实现

   private Dictionary<string, AssetBundle> loadedAssetBundles = new Dictionary<string, AssetBundle>();
    public static AssetBundleManager _instance;  //变量
    
    //使用属性不需要关联游戏对象，使用awake必须关联游戏对象，此脚本属于帮助类使用属性更适合 
    public static AssetBundleManager Instance   //属性
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AssetBundleManager>();
                if(_instance == null)
                {
                       GameObject go = new GameObject("AssetBundleManager");  //创建游戏对象名字为""内的字符串
                       _instance =  go.AddComponent<AssetBundleManager>(); 
                       DontDestroyOnLoad(_instance);  //可能多个场景需要ab包
                }
            }
                return _instance;
        }

        //未写set该属性是只读的
    }

//   private void Awake()
//     {
//         if(instance == null)
//         {    
//             instance = this;
//             return;
//         }    
//         DontDestroyOnLoad(instance);
//     }

//     Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(string bundleBasePath)
    {
        this.bundleBasePath =  bundleBasePath; 
        AssetBundle mainBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+"Windows");
        if(mainBundle == null)
        {
            Debug.Log("加载主包失败");
        }
        else
        {
            manifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
    }
    private AssetBundle LoadAssetBundle(string assetBundleName)
    {
        if(loadedAssetBundles.TryGetValue(assetBundleName, out AssetBundle assetBundle))  //判断并取值
        {
            return assetBundle;
        }
        assetBundle = AssetBundle.LoadFromFile(bundleBasePath+"/"+assetBundleName);
        print(bundleBasePath+"/"+assetBundleName);
        if(assetBundle == null)
        {
            Debug.Log("加载 "+assetBundleName+" 失败");
            return null;
        }
        loadedAssetBundles.Add(assetBundleName,assetBundle);        //将加载的AB包添加到字典中
        return assetBundle;
    }   
    
    /// <summary>
    /// assetBundleName--包名  assetName---包中的资产
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetBundleName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns> <summary>
    public T LoadAsset<T>(string assetBundleName,string assetName) where T :Object//限制T是Object或它的的子类
    {
        //1.获取依赖--->2.加载资源包--->3.加载资源
        //1. 
        LoadDependencies(assetBundleName);
        //2  
        AssetBundle assetBundle =  LoadAssetBundle(assetBundleName);
         //3.加载资源
        {
            T asset = assetBundle.LoadAsset<T>(assetName);
            return asset;
        }
    }
        

    private void LoadDependencies(string assetBundleName)//1.加载依赖
    {
       string[] dependencies = manifest.GetAllDependencies(assetBundleName);
       foreach(string dependency in dependencies)
       {
            if(!loadedAssetBundles.ContainsKey(dependency))//判断是否有dependence
            {
                AssetBundle.LoadFromFile(bundleBasePath+"/"+dependency);
            }
       }
    }

    private IEnumerator LoadAssetBundleAsync(string assetBundleName)
    {
        if(loadedAssetBundles.ContainsKey(assetBundleName))  //判断并取值
        {
            // return assetBundle;   确定有
            yield break;
        }
        //先等待加载依赖
        yield return LoadDependenciesAsync(assetBundleName);

        //判断是网络加载还是本地加载
        if(bundleBasePath.StartsWith("http://")||bundleBasePath.StartsWith("https://"))
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleBasePath+"/AssetBundles/"+assetBundleName);
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("网络异步加载 "+assetBundleName+" 失败");
                yield break;
            }
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);    

           AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);         
           loadedAssetBundles.Add(assetBundleName, assetBundle);
        }
        else
        {
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundleBasePath+"/"+assetBundleName);   //ab包  
            yield return request;
            AssetBundle assetBundle = request.assetBundle;  
            loadedAssetBundles.Add(assetBundleName, assetBundle); 
        }   
    }

    private IEnumerator LoadDependenciesAsync(string assetBundleName)
    { 
       string[] dependencies = manifest.GetAllDependencies(assetBundleName);
       foreach(string dependency in dependencies)
       {
        // 形成递归
           if(!loadedAssetBundles.ContainsKey(dependency))//判断是否有dependence
            {
                yield return LoadAssetBundleAsync(dependency);
            }
       }
        // yield return null;
    }

    IEnumerator WaitForLoadAsset<T>(string assetBundleName,string assetName,System.Action<T> onComplete) where T :Object
    {
        //1.先加载ab包
        yield return LoadAssetBundleAsync(assetBundleName);
        
        //2.加载资产
        AssetBundle assetBundle = loadedAssetBundles[assetBundleName];
        AssetBundleRequest request = assetBundle.LoadAssetAsync<T>(assetName);
        yield return request;  
        T asset = request.asset as T;
        onComplete.Invoke(asset);    //将asset传给onComplete然后至TestABManager中的委托
    }

    public void LoadAssetAsync<T>(string assetBundleName,string assetName,System.Action<T> onComplete) where T :Object
    {
        StartCoroutine(WaitForLoadAsset<T>(assetBundleName,assetName,onComplete));
    }

}
