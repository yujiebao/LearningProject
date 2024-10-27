using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tutorial;
using UnityEngine;
using XLua;
using System;
using System.Runtime.InteropServices;
public class BaseClass
{
    public static void BSFunc()
    {
        Debug.Log("BaseClass::BSFunc");
    }
    public static int BSF = 8866;
    
    public void BMFunc()
    {
        Debug.Log("BaseClass::BMFunc");
    }
}

public class DerivedClass : BaseClass
{
    public static void DSFunc()
    {
        Debug.Log("DerivedClass::DSFunc");
    }

    public int DMF{get;set;}

    public void DMFunc()
    {
        Debug.Log("DerivedClass::DMFunc");
    }

    public double ComplexFunc(Param1 p1 ,ref int p2, out string p3, Action luafunc, out Action<string> csfunc)
    {  //无意义的复杂代码仅演示Lua调用CS代码
       Debug.Log("P1 = {x="+p1.x+",y="+p1.y+"},p2 = "+p2);
       luafunc();
       p2 = p2 * p1.x;
       p3 = "Hello "+ p1.y;
       csfunc = (a) =>
       {
        Debug.Log(a);
           Debug.Log("csharp callback invoked!");
        //    print("hello");
       };
       return 1.23f;
    }

    public Action<string> TestDelegate = (param) =>
    {
        Debug.Log("TestDelegate in C# : "+param);
    };
}

public class LuaCallCSharp : MonoBehaviour
{
    private LuaEnv luaEnv = null;
    private void Start() {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'LuaCallCSharp'");  //加载模块

        // new GameObject("CSharp GameObject");

        // print("CS Time: "+Time.deltaTime);

        // GameObject.Find("Cube").SetActive(false);

        // BaseClass.BSFunc();

        // DerivedClass dc = new DerivedClass();
        // dc.BMFunc();
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
