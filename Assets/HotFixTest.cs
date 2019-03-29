using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix][LuaCallCSharp]
public class HotFixTest : MonoBehaviour {

    LuaEnv luaenv = new LuaEnv();

    private int tick = 0;
    private string str = null;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if(++tick % 50 == 0)
        {
            Debug.Log(">>>>>>>>>>>>>>>Update in C#,tick = " + tick);
            Debug.Log(">>>>>>>>>>>>>>>FixUpdate " + str);
        }
    }

    [LuaCallCSharp]
    public void FixUpdate()
    {

    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,300,80), "HotFix"))
        {
            //luaenv.DoString(@"
            //    xlua.hotfix(CS.HotFixTest, 'Update', function(self)
            //        self.tick = self.tick +1
            //        if (self.tick % 50) == 0 then
            //            print('>>>>>>>>>>>>Update in Lua,tick = '..self.tick)
            //        end
            //    end)
            //");
            luaenv.DoString("require 'hotfix'");
        }
    }
}
