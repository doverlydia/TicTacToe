using UnityEngine;
using UnityEngine.UI;

public class PawnPlayerAssigner : MonoBehaviour
{
    [SerializeField] private GraphicsStore _store;
    public GraphicsStore store => _store;
    public static PawnPlayerAssigner Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


}
