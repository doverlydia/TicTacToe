using UnityEngine;
using UnityEngine.UI;

public class PawnPlayerAssigner : MonoBehaviour
{
    public static PawnPlayerAssigner instance { get; private set; }

    private Sprite Ximage;
    private Sprite Oimage;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }


}
