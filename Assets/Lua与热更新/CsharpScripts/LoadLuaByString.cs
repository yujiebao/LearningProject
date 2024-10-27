using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 通过字符串加载Lua脚本
/// </summary>
public class LoadLuaByString : MonoBehaviour
{
    private LuaEnv luaEnv = null;
    private void Start() {
        luaEnv = new LuaEnv();   //创建一个lua环境
        // string a = "abc";
        // string luaScript = "print('hello world')";  //用c#语言创建的lua环境,print('hello world')为lua代码
        //@逐字符  逐字符字符串常用于包含路径、正则表达式、多行文本等场景，可以避免手动转义字符的麻烦。
        string luaScript =@"
        a = 4
        b = 5 
        print(a+b)
        ";
        luaEnv.DoString(luaScript);

    }
    private void OnDestroy()   //OnDestroyunity生命周期的函数
    {
        luaEnv.Dispose();//释放lua环境    
    }
}
