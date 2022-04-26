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
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private Sprite Ximage;
    [SerializeField] private Sprite Oimage;
    [SerializeField] private Image turnImage;

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
