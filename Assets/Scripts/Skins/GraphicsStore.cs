using UnityEngine;
[CreateAssetMenu(fileName = "GraphicsStore", menuName = "ScriptableObjects/GraphicStore", order = 1)]
public class GraphicsStore : ScriptableObject
{
    public Sprite XSprite { get; private set; }
    public Sprite Osprite { get; private set; }
    public Sprite BGsprite { get; private set; }

    public void SetStore(Texture2D xSprite, Texture2D oSprite, Texture2D bgSprite)
    {
        this.XSprite = ConvertTextureToSprite(xSprite);
        this.Osprite = ConvertTextureToSprite(oSprite);
        this.BGsprite = ConvertTextureToSprite(bgSprite);
    }
    private Sprite ConvertTextureToSprite(Texture2D texture)
    {
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture, rec, new Vector2(0, 0), 0.1f);
    }
}
