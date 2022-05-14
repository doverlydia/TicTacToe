using UnityEditor;
using UnityEngine;
public class AssetBundleGenerator : EditorWindow
{
    string skinName;
    Texture2D xSprite;
    Texture2D oSprite;
    Texture2D bgSprite;
    [MenuItem("AssetBundles/AssetBundleGenerator")]
    public static void ShowWindow()
    {
        EditorWindow wnd = EditorWindow.GetWindow(typeof(AssetBundleGenerator));
        wnd.minSize = new Vector2(450, 300);
        wnd.maxSize = new Vector2(1920, 720);
    }

    private void OnGUI()
    {
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        AssetBundleBuild newSkin = new AssetBundleBuild();

        skinName = EditorGUILayout.TextField("Bundle name: ", skinName);

        string[] skinAssetsPaths = new string[3];

        xSprite = (Texture2D)EditorGUILayout.ObjectField("X texture", xSprite, typeof(Texture2D), false);
        oSprite = (Texture2D)EditorGUILayout.ObjectField("O texture", oSprite, typeof(Texture2D), false);
        bgSprite = (Texture2D)EditorGUILayout.ObjectField("BG texture", bgSprite, typeof(Texture2D), false);

        skinAssetsPaths[0] = AssetDatabase.GetAssetPath(xSprite);
        skinAssetsPaths[1] = AssetDatabase.GetAssetPath(oSprite);
        skinAssetsPaths[2] = AssetDatabase.GetAssetPath(bgSprite);

        newSkin.assetBundleName = skinName;
        newSkin.assetNames = skinAssetsPaths;
        newSkin.addressableNames = new string[3] { SkinAdressableNames.xSpriteAdressableName,
                                                   SkinAdressableNames.oSpriteAdressableName,
                                                   SkinAdressableNames.bgSpriteAdressableName };

        buildMap[0] = newSkin;

        if (GUILayout.Button("Create Asset Bundle"))
        {
            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, buildMap, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            xSprite = null;
            oSprite = null;
            bgSprite = null;
            skinName = null;
        }
    }
}