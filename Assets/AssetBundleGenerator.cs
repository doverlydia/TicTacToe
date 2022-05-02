using UnityEditor;

using UnityEngine;
using System.IO;

public class AssetBundleGenerator : EditorWindow
{
    private string bundleName;
    private Texture2D xSprite;
    private Texture2D oSprite;
    private Texture2D bgSprite;
    private string directory = Application.streamingAssetsPath;


    [MenuItem("AssetBundles/AssetBundleGenerator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AssetBundleGenerator));
    }

    private void OnGUI()
    {
        bundleName = EditorGUILayout.TextField("Bundle name: ", bundleName);
        xSprite = (Texture2D)EditorGUILayout.ObjectField("X texture", xSprite, typeof(Texture2D), false);
        oSprite = (Texture2D)EditorGUILayout.ObjectField("O texture", oSprite, typeof(Texture2D), false);
        bgSprite = (Texture2D)EditorGUILayout.ObjectField("BG texture", bgSprite, typeof(Texture2D), false);

        if (GUILayout.Button("Create Asset Bundle"))
        {
            Object[] bb = new Object[3];
            bb[0] = xSprite;
            bb[1] = oSprite;
            bb[2] = bgSprite;
            BuildPipeline.BuildAssetBundle(xSprite, bb, $"{directory}/{bundleName}", BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
    }
}