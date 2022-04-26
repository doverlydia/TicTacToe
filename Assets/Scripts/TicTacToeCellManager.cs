using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeCellManager : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] private Sprite imageX;
    [SerializeField] private Sprite imageO;
    [SerializeField] private Image cellImage;
    [SerializeField] private Vector2 _cellLocationOnGrid;
    private bool cellEmpty = true;
    public Vector2 cellLocationOnGrid => _cellLocationOnGrid;

    public void PlayerInput()
    {
        if (gameStatus.Xturn)
        {
            UpdateCellStatus();
        }
    }
    public void UpdateCellStatus()
    {
        string player;

        if (cellEmpty && !gameStatus.gameEnded)
        {
            if (gameStatus.Xturn)
            {
                cellImage.sprite = imageX;
                player = "X";
            }
            else
            {
                cellImage.sprite = imageO;
                player = "O";
            }
            gameStatus.UpdateGameStatus(_cellLocationOnGrid, player);
        }
        cellEmpty = false;
    }
    public void ClearCells()
    {
        cellEmpty = true;
        cellImage.sprite = null;
    }
}
