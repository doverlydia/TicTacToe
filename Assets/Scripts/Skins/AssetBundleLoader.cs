using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField] GraphicsStore graphicsStore;
    [SerializeField] TMP_Text feedback;
    [SerializeField] TMP_Dropdown dropdown;
    private void OnEnable()
    {
        feedback.text = "";
        CreateBundlesDropDownSelector();
    }
    public void Reskin()
    {
        AssetBundle skin = LoadSkin();
        if (skin == null)
        {
            feedback.text = "There is no such skin.";
        }
        else
        {
            graphicsStore.SetStore(skin.LoadAsset<Texture2D>(SkinAdressableNames.xSpriteAdressableName),
                                   skin.LoadAsset<Texture2D>(SkinAdressableNames.oSpriteAdressableName),
                                   skin.LoadAsset<Texture2D>(SkinAdressableNames.bgSpriteAdressableName));
            feedback.text = "skin loaded succefully!";
            skin.Unload(false);
        }
    }

    private AssetBundle LoadSkin()
    {
        string skinName = GetChosenOption();
        if (File.Exists($"{Application.streamingAssetsPath}/{skinName}"))
        {
            AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile($"{Application.streamingAssetsPath}/{skinName}");
            return myLoadedAssetBundle;
        }
        else
        {
            return null;
        }
    }

    private HashSet<string> GetNames()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dir.GetFiles("*.*");
        HashSet<string> bundleNames = new HashSet<string>();

        foreach (FileInfo f in info)
        {
            string name = f.Name.Split('.')[0];
            if (name != "StreamingAssets")
                bundleNames.Add(name);
        }

        return bundleNames;
    }

    private void CreateBundlesDropDownSelector()
    {
        HashSet<string> bundleNames = GetNames();
        dropdown.ClearOptions();
        dropdown.AddOptions(bundleNames.ToList());
    }

    private string GetChosenOption()
    {
        return dropdown.options[dropdown.value].text;
    }
}