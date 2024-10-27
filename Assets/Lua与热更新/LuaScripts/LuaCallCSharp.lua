--动态创建GameObject
--CS.UnityEngine.GameObject("Lua GameObject")

--访问静态方法
-- print("CS Time: ",CS.UnityEngine.Time.deltaTime)


-- CS.UnityEngine.GameObject.Find("Cube").SetActive(false)此方法不对
-- CS.UnityEngine.GameObject.Find("Cube"):SetActive(false)--改用此方法


-- 访问C#中父类成员
-- CS.BaseClass:BSFunc()
-- CS.BaseClass.BSFunc()   --无命名空间
-- print(CS.BaseClass.BSF)
-- bc = CS.BaseClass()
-- bc:BMFunc()

-- CS.DerivedClass.DSFunc()
dc = CS.DerivedClass()
-- dc:BMFunc()
-- dc.DMF = 1024
-- print(dc.DMF)
-- print(CS.DerivedClass.BSF)
-- dc:DMFunc()
ret,p2,p3,csfunc = dc:ComplexFunc({x=3,y = "john"},100,function () print("I am Lua CallBack")  end)
print(ret,p2,p3)
csfunc(10086);
dc.TestDelegate("Hello ");