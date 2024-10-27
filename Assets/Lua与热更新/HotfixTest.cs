using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using UnityEngine.UI;
[Hotfix]
public class HotfixTest : MonoBehaviour
{
    private LuaEnv luaEnv = null;
    public Button button;
    private void Start() {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'Test'");
        button.onClick.AddListener(()=>{
            luaEnv.DoString("require 'Hotfix'");
        });
    }

    private void Update() {
        print(">>>Update in C#: "+Time.deltaTime);
    }
    private byte[] MyLoader(ref string fileName)
    {
        string filePath = Application.dataPath+"/Lua与热更新/LuaScripts/"+fileName+".lua";
        return File.ReadAllBytes(filePath);
        // return System.Text.Encoding.UTF8.GetBytes(filePath);//文件中写有中文
    }
    private void OnDestroy()   //OnDestroyunity生命周期的函数
    {
        luaEnv.Dispose();//释放lua环境    
    }
}
