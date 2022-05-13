using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BlackScreenUtils blackScreen;
    [SerializeField] private ModeData modeData;
    [SerializeField] private GraphicsStore store;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform gameGrid;
    [SerializeField] private Image bg;
    [SerializeField] private PopUpUtils gameEndPopUp;
    [SerializeField] private TMP_Text feedback;
    [SerializeField] private TMP_Text whosTurn;
    [SerializeField] private GameObject hintButton;
    [SerializeField] private GameObject undoButton;
    private readonly Button[,] board = new Button[3, 3];

    private readonly GameLogic gameLogic = new GameLogic();

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
    private void Start()
    {
        hintButton.SetActive(modeData.GameMode == GameMode.playerVScomputer);
        undoButton.SetActive(modeData.GameMode == GameMode.playerVScomputer);
        CreateBoard();
        if (modeData.GameMode != GameMode.computerVScomputer)
        {
            InitBoard();
        }
        else
        {
            StartCoroutine(AIvsAI());
        }
    }

    public void RestartGame()
    {
        gameLogic.RestartGame();
        UpdateBoard();
        UpdateWhosTurn();
    }

    public void Hint()
    {
        Debug.Log(gameLogic.Hint());
    }

    public void Undo()
    {
        gameLogic.Undo();
        UpdateBoard();
    }

    private void InitBoard()
    {
        bg.sprite = store.BGsprite;

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                Coordinate cord = new Coordinate(r, c);
                board[r, c].onClick.AddListener(() => gameLogic.ConcludeTurn(cord));
                board[r, c].onClick.AddListener(() => UpdateCell(cord));
                board[r, c].onClick.AddListener(() => gameLogic.ChangeTurn());
                board[r, c].onClick.AddListener(() => UpdateWhosTurn());

                if (modeData.GameMode == GameMode.playerVScomputer)
                {
                    board[r, c].onClick.AddListener(() => StartCoroutine(AI_Turn()));
                }

                board[r, c].onClick.AddListener(() => IsGameEnded());
            }
        }

        UpdateWhosTurn();
    }

    public void CreateBoard()
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                board[r, c] = Instantiate(cellPrefab, gameGrid).GetComponent<Button>();
            }
        }
    }

    private IEnumerator AIvsAI()
    {
        SetBoardInteractive(false);
        while (!IsGameEnded())
        {
            Coordinate AI_move = new Coordinate(gameLogic.Hint());
            gameLogic.ConcludeTurn(AI_move);
            UpdateCell(AI_move);
            gameLogic.ChangeTurn();
            UpdateWhosTurn();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateWhosTurn()
    {
        whosTurn.text = gameLogic.WhosTurn == PawnType.X ? $"It's {modeData.Player1Name}'s Turn!" : $"It's {modeData.Player2Name}'s Turn!";
    }

    private void UpdateCell(Coordinate coordinate)
    {
        switch (gameLogic.Board[coordinate.R, coordinate.C])
        {
            case PawnType.None:
                board[coordinate.R, coordinate.C].image.sprite = null;
                board[coordinate.R, coordinate.C].image.color = Color.clear;
                SetCellInteractive(coordinate, true);
                return;
            case PawnType.X:
                board[coordinate.R, coordinate.C].image.sprite = store.XSprite;
                board[coordinate.R, coordinate.C].image.color = Color.white;
                SetCellInteractive(coordinate, false);
                return;
            case PawnType.O:
                board[coordinate.R, coordinate.C].image.sprite = store.Osprite;
                board[coordinate.R, coordinate.C].image.color = Color.white;
                SetCellInteractive(coordinate, false);
                return;
        }

    }

    private void UpdateBoard()
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                UpdateCell(new Coordinate(r, c));
            }
        }
    }

    private void OnGameEnded()
    {
        blackScreen.FadeBlackScreen(0.95f);
        switch (gameLogic.GameState)
        {
            case (GameState.Draw):
                feedback.text = "It's A Draw!";
                break;
            case (GameState.WinnerO):
                feedback.text = $"{modeData.Player2Name} Is The Winner!";
                break;
            case (GameState.WinnerX):
                feedback.text = $"{modeData.Player1Name} Is The Winner!";
                break;
        }
        gameEndPopUp.PopIn();
    }

    private bool IsGameEnded()
    {
        if (EnumUtils.IsGameEnded(gameLogic.GameState))
        {
            OnGameEnded();
            SetBoardInteractive(false);
            return true;
        }
        return false;
    }

    private IEnumerator AI_Turn()
    {
        SetBoardInteractive(false);
        yield return new WaitForSeconds(1);
        if (!EnumUtils.IsGameEnded(gameLogic.GameState))
        {
            Coordinate AI_move = new Coordinate(gameLogic.Hint());
            gameLogic.ConcludeTurn(AI_move);
            UpdateCell(AI_move);
            gameLogic.ChangeTurn();
            UpdateWhosTurn();
            if (!IsGameEnded())
                SetBoardInteractive(true);
        }
    }

    public void SetCellInteractive(Coordinate coordinate, bool isInteractive)
    {
        board[coordinate.R, coordinate.C].interactable = isInteractive;
    }

    public void SetBoardInteractive(bool isInteractive)
    {
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (gameLogic.Board[r, c] == PawnType.None)
                    SetCellInteractive(new Coordinate(r, c), isInteractive);
            }
        }
    }
}
