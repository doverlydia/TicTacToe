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
    [SerializeField] private Button button;
    private bool cellEmpty = true;
    public Vector2 cellLocationOnGrid => _cellLocationOnGrid;

    private void Start()
    {
        cellImage.color = new Color(255, 255, 255, 0);
    }

    public void PlayerInput()
    {
        if (gameStatus.Xturn || !gameStatus.againstAI)
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
            cellImage.color = new Color(255, 255, 255, 255);
            gameStatus.UpdateGameStatus(_cellLocationOnGrid, player);
        }
        cellEmpty = false;
    }
    public void ClearCell()
    {
        cellEmpty = true;
        cellImage.sprite = null;
        cellImage.color = new Color(255, 255, 255, 0);
    }
    public IEnumerator Flash(float inTime, float outTime, int numberOfFlashes, string XO)
    {
        button.interactable = false;
        cellImage.sprite = XO == "X" ? imageX : imageO;
        cellImage.color = new Color(255, 255, 255, 255);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(outTime);
            cellImage.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(inTime);
            cellImage.color = new Color(255, 255, 255, 255);
        }
        cellImage.sprite = null;
        cellImage.color = new Color(255, 255, 255, 0);
        button.interactable = true;
    }
}
