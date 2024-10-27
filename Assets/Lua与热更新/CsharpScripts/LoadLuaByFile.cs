using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LoadLuaByFile : MonoBehaviour
{
    private LuaEnv luaEnv = null;
    private void Start() {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'LuaFile'");   //DoString将后面的字符串视作Lua代码执行，可不写扩展名
                                                //加载文件的扩展名.lua.txt
    }
    private void OnDestroy()   //OnDestroyunity生命周期的函数
    {
        luaEnv.Dispose();//释放lua环境    
    }
}
