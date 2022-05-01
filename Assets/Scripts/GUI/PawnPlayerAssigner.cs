using UnityEngine;
using UnityEngine.UI;

public class PawnPlayerAssigner : MonoBehaviour
{
    [SerializeField] private Sprite Ximage;
    [SerializeField] private Sprite Oimage;
    [SerializeField] private Image turnImage;
    
    PawnType player1;
    PawnType player2 => player1 == PawnType.X ? PawnType.O : PawnType.X;

}
