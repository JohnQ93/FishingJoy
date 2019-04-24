using UnityEditor;
using System.IO;

public class CreateAssetBundles {

    [MenuItem("Assets/Create AssetBundles")]
    public static void CreateAB()
    {
        string dir = "AssetBundles";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
