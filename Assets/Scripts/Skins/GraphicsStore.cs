using UnityEngine;
[CreateAssetMenu(fileName = "GraphicsStore", menuName = "ScriptableObjects/GraphicStore", order = 1)]
public class GraphicsStore : ScriptableObject
{

    private Sprite _xSprite;
    private Sprite _oSprite;
    private Sprite _bgSprite;

    [SerializeField] private Sprite defaultXSprite;
    [SerializeField] private Sprite defaultOsprite;
    [SerializeField] private Sprite defaultBGsprite;

    public Sprite XSprite
    {
        get
        {
            return _xSprite == null ? defaultXSprite : _xSprite;
        }
        private set
        {
            _xSprite = value;
        }
    }
    public Sprite Osprite
    {
        get
        {
            return _oSprite == null ? defaultOsprite : _oSprite;
        }
        private set
        {
            _oSprite = value;
        }
    }
    public Sprite BGsprite
    {
        get
        {
            return _bgSprite == null ? defaultBGsprite : _bgSprite;
        }
        private set
        {
            _bgSprite = value;
        }
    }

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
