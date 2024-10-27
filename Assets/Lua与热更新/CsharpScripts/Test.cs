// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
// using XLua;
// // public class luaClass
// //     {
// //         public int f1;
// //         public int f2;
// //         public int add(int a , int b)   //优先执行类中函数而不执行Lua中的函数，除非类中没有方法覆盖  难以映射  使用接口
// //         {
// //             return a + b;
// //             Debug.Log("CSharp");
// //         }
// //     }

// // //使用接口可以映射lua中的table且可以执行lua中的函数
// [CSharpCallLua]  //在unity中的XLua中generate代码会生成ILuaClass接口
// public interface ILuaClass
// {
//     int f1{get;set;}
//     int f2{get;set;}
//     int add(int a , int b);
// }    
// public class Test : MonoBehaviour
// {
    
//     private LuaEnv luaEnv = null;
//     private void Start() {
//         luaEnv = new LuaEnv();
//         luaEnv.AddLoader(MyLoader);
//         luaEnv.DoString("require 'CSharpCallLua'");
        
//         // 获取lua中的全局基本类型变量
//         // int a = luaEnv.Global.Get<int>("a");
//         // print("a = "+a );
    
//         //  luaClass dd = luaEnv.Global.Get<luaClass>("d");
//         //  Debug.Log(dd.f1 + " "+dd.f2+dd.add(1,2) );
      
//         // 获取lua中的表table
//         // ILuaClass dd = luaEnv.Global.Get<ILuaClass>("d");
//         // Debug.Log(dd.f1+"f1,f2 = " +dd.f2);

//         // // 使用列表映射table的数组
//         // List<int> list = luaEnv.Global.Get<List<int>>("d");
//         // foreach(int item in list)
//         // {
//         //     Debug.Log(item);
//         // }

//         // 使用字典进行映射表 无法映射方法
//         Dictionary<string,int> dic = luaEnv.Global.Get<Dictionary<string,int>>("d");
//         Debug.Log(dic["f1"]+","+dic["f2"]);

//         //使用delegate映射table中的方法
//         Action a = luaEnv.Global.Get<Action>("e");
//         a();
//     }

//     /// <summary>
//     /// 此方法作为
//     /// </summary>
//     /// <param name="fileName"></param>
//     /// <returns></returns>
//     private byte[] MyLoader(ref string fileName)
//     {
//         string filePath = Application.dataPath+"/Lua与热更新/LuaScripts/"+fileName+".lua";
//         print(fileName);
//         return File.ReadAllBytes(filePath);
//         // return System.Text.Encoding.UTF8.GetBytes(filePath);//文件中写有中文
//     }
// }
