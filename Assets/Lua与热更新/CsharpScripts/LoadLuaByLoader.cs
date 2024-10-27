using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using XLua;

/// <summary>
/// 自定义加载器，加载lua文件
/// </summary>
public class LoadLuaByLoader : MonoBehaviour
{
    private LuaEnv luaEnv = null;
    private void Start() {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'Test'");

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
