using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class HotFixScript : MonoBehaviour {

    private LuaEnv luaEnv;

    public static Dictionary<string, GameObject> fishDic = new Dictionary<string, GameObject>();

    void Awake () {
        luaEnv = new LuaEnv();

        luaEnv.AddLoader(MyLoader);

        luaEnv.DoString("require'fish'");
    }

    private byte[] MyLoader(ref string filePath)
    {
        string absPath = Path.Combine(Application.dataPath , "..") + "/PlayerGamePackage/" + filePath + ".lua.txt";
        //return File.ReadAllBytes(absPath);
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }

    private void OnDisable()
    {
        luaEnv.DoString("require'fishDispose'");
    }

    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

    [LuaCallCSharp]
    public static void LoadResource(string resName, string filePath)
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(@"E:\Unity2018Projects\Learning\FishingJoy\AssetBundles\" + filePath);
        GameObject go = assetBundle.LoadAsset<GameObject>(resName);
        fishDic.Add(resName, go);
    }
    [LuaCallCSharp]
    public static GameObject GetResource(string resName)
    {
        return fishDic[resName];
    }
}
