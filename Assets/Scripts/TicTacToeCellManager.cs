using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TicTacToeCellManager : MonoBehaviour
{
    [SerializeField] private GameStatus gameStatus;
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
    public void ClearCell()
    {
        cellEmpty = true;
        cellImage.sprite = null;
    }
    //public IEnumerator Flash(float fadeinOutTime, int numberOfFlashes, string XO)
    //{
    //    cellImage.sprite = XO == "X" ? imageX : imageO;
    //    Tween fadeIn = cellImage.DOFade(255, fadeinOutTime);
    //    Tween fadeOut = cellImage.DOFade(0, fadeinOutTime);
    //    fadeIn.Play();
    //    fadeIn.OnComplete(() => fadeOut.Play());
    //    cellImage.sprite = null;
    //    yield return null;
    //}
}
