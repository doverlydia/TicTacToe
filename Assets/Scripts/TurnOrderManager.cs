using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderManager : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] Sprite Ximage;
    [SerializeField] Sprite Oimage;
    [SerializeField] Image turnImage;

    private void Start()
    {
        turnImage.sprite = Ximage;
    }
    public void ChangeTurnOrder()
    {
        if (!gameStatus.gameStarted)
        {
            gameStatus.UpdateGameStatus(Vector2.zero, "");
            turnImage.sprite = Oimage;
        }
    }
}
