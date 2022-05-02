using UnityEngine;
[CreateAssetMenu(fileName = "GraphicsStore", menuName = "ScriptableObjects/GraphicStore", order = 1)]
public class GraphicsStore : ScriptableObject
{
    private Texture2D xSprite;
    private Texture2D oSprite;
    private Texture2D bgSprite;

    public void SetStore(Texture2D xSprite, Texture2D oSprite, Texture2D bgSprite)
    {
        this.xSprite = xSprite;
        this.oSprite = oSprite;
        this.bgSprite = bgSprite;
    }
}
