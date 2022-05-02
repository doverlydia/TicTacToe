using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField] GraphicsStore graphicsStore;
    [SerializeField] TMP_InputField skinName;

    public void Reskin()
    {
        AssetBundle skin = LoadSkin(skinName.text);
        if (skin != null)
        {
            graphicsStore.SetStore(skin.LoadAsset<Texture2D>(SkinAdressableNames.xSpriteAdressableName),
                                   skin.LoadAsset<Texture2D>(SkinAdressableNames.oSpriteAdressableName),
                                   skin.LoadAsset<Texture2D>(SkinAdressableNames.bgSpriteAdressableName));
        }
    }

    private AssetBundle LoadSkin(string skinName)
    {
        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile($"{Application.streamingAssetsPath}/{skinName}");

        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return null;
        }
        myLoadedAssetBundle.Unload(false);
        return myLoadedAssetBundle;
    }
}