using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class AssetBundleCreatorWindow : EditorWindow
{
    public List<GameObject> assetList = new List<GameObject>();
    public List<BuildTarget> buildTargetList = new List<BuildTarget>();
    public string assetBundleName = "Name your AssetBundle here.";
    public bool useVariant;
    public string variantName = "Name your Variant here.";
    public bool useCustomPath;
    public string assetBundleSavePath = Application.streamingAssetsPath;
    public bool useOptions;
    public BuildAssetBundleOptions options;

    [MenuItem("Window/Assets/AssetBundleCreator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<AssetBundleCreatorWindow>("AssetBundleCreator");
    }

    private void Awake()
    {
        buildTargetList.Add(EditorUserBuildSettings.activeBuildTarget);
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        assetBundleName = GUILayout.TextField(assetBundleName);

        useVariant = EditorGUILayout.BeginToggleGroup("Use a custom Variant?", useVariant);
        variantName = GUILayout.TextField(variantName);
        EditorGUILayout.EndToggleGroup();

        useCustomPath = EditorGUILayout.BeginToggleGroup("Use a custom save path?", useCustomPath);
        assetBundleSavePath = GUILayout.TextField(assetBundleSavePath);
        EditorGUILayout.EndToggleGroup();

        useOptions = EditorGUILayout.BeginToggleGroup("Use custom options?", useOptions);
        options = (BuildAssetBundleOptions)EditorGUILayout.EnumFlagsField(options);
        EditorGUILayout.EndToggleGroup();

        var serializedObject = new SerializedObject(this);

        //assetlist 
        EditorGUILayout.LabelField("Your assets you want to include in this AssetBundle.");
        var assetListProperty = serializedObject.FindProperty("assetList");
        serializedObject.Update();
        EditorGUILayout.PropertyField(assetListProperty, true);
        serializedObject.ApplyModifiedProperties();
        //endlist

        //build target list
        EditorGUILayout.LabelField("Your platforms you want to deploy to.");
        serializedObject.Update();
        var buildTargetListProperty = serializedObject.FindProperty("buildTargetList");
        EditorGUILayout.PropertyField(buildTargetListProperty, true);
        serializedObject.ApplyModifiedProperties();
        //endlist

        if (GUILayout.Button("Create Asset Bundle"))
        {
            CreateAssetBundle();
        }
    }

    private void CreateAssetBundle()
    {
        if (!Directory.Exists(assetBundleSavePath))
        {
            Directory.CreateDirectory(assetBundleSavePath);
        }
        foreach (var buildTarget in buildTargetList)
        {
            if (!Directory.Exists(assetBundleSavePath + "/" + assetBundleName + "/" + buildTarget.ToString()))
            {
                Directory.CreateDirectory(assetBundleSavePath + "/" + assetBundleName + "/" + buildTarget.ToString());
            }

            foreach (var gObject in assetList)
            {
                var assetImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(gObject));
                assetImporter.assetBundleName = assetBundleName;
                if (useVariant)
                {
                    assetImporter.assetBundleVariant = variantName;
                }
            }
            BuildPipeline.BuildAssetBundles(assetBundleSavePath + "/" + assetBundleName + "/" + buildTarget.ToString(), options, buildTarget);
        }
    }
}