using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using UnityEngine.Networking;

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
        //string absPath = Path.Combine(Application.dataPath , "..") + "/PlayerGamePackage/" + filePath + ".lua.txt";
        string absPath = @"E:\Unity2018Projects\Learning\FishingJoy\PlayerGamePackage\" + filePath + ".lua.txt";
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
    public void LoadResource(string resName, string filePath)
    {
        StartCoroutine(LoadResourceCoroutine(resName, filePath));
    }

    IEnumerator LoadResourceCoroutine(string resName, string filePath)
    {
        UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(@"http://localhost/AssetBundles/" + filePath);
        yield return webRequest.SendWebRequest();
        AssetBundle assetBundle = (webRequest.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        GameObject go = assetBundle.LoadAsset<GameObject>(resName);
        fishDic.Add(resName, go);
    }

    [LuaCallCSharp]
    public static GameObject GetResource(string resName)
    {
        return fishDic[resName];
    }


}
