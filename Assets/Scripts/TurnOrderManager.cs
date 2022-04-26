using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum turnSprite
{
    X,
    O
}
public class TurnOrderManager : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] Sprite Ximage;
    [SerializeField] Sprite Oimage;
    [SerializeField] Image turnImage;

    private void Start()
    {
        ChangeTurnSprite(turnSprite.X);
    }
    public void ChangeTurnOrder()
    {
        if (!gameStatus.gameStarted)
        {
            gameStatus.UpdateGameStatus(Vector2.zero, "");
            ChangeTurnSprite(turnSprite.O);
        }
    }
    public void ChangeTurnSprite(turnSprite sprite)
    {
        switch (sprite)
        {
            case turnSprite.X:
                turnImage.sprite = Ximage;
                return;
            case turnSprite.O:
                turnImage.sprite = Oimage;
                return;
        }
    }
}
