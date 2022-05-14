using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField] GraphicsStore graphicsStore;
    [SerializeField] TMP_InputField skinName;
    [SerializeField] TMP_Text feedback;
    private void OnEnable()
    {
        feedback.text = "";
        skinName.text = "";
        GetNames();
    }
    public void Reskin()
    {
        AssetBundle skin = LoadSkin(skinName.text);
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

    private AssetBundle LoadSkin(string skinName)
    {
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
            bundleNames.Add(Path.GetFileNameWithoutExtension($"{Application.streamingAssetsPath}/{f.Name}"));
        }

        return bundleNames;
    }
}