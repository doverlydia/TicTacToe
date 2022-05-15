using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BlackScreenUtils blackScreen;
    [SerializeField] private ModeData modeData;
    [SerializeField] private GraphicsStore store;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform gameGrid;
    [SerializeField] private Image bg;
    [SerializeField] private PopUpUtils gameEndPopUp;
    [SerializeField] private TMP_Text feedback;
    [SerializeField] private TMP_Text whosTurn;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text countdownText;
    private readonly Button[,] board = new Button[3, 3];
    private readonly GameLogic gameLogic = new GameLogic();
    private bool isAIturn;

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
        hintButton.gameObject.SetActive(modeData.GameMode == GameMode.playerVScomputer);
        undoButton.gameObject.SetActive(modeData.GameMode == GameMode.playerVScomputer);
        CreateBoard();
        if (modeData.GameMode != GameMode.computerVScomputer)
        {
            InitBoard();
            StartCoroutine(CountDown(3, () => StartCoroutine(TurnTimer(5))));
        }
        else
        {
            StartCoroutine(CountDown(3, () => StartCoroutine(AIvsAI())));
        }

    }

    public void RestartGame()
    {
        gameLogic.RestartGame();
        modeData.RandomAsignPlayers();
        UpdateBoard();
        UpdateWhosTurn();
        StopAllCoroutines();
        StartCoroutine(TurnTimer(5));
    }

    public void Hint()
    {
        StartCoroutine(ShowHint());
    }

    IEnumerator ShowHint()
    {
        Coordinate hintCord = gameLogic.Hint();
        if (!EnumUtils.IsGameEnded(gameLogic.GameState) && !isAIturn && !DOTween.IsTweening(board[hintCord.R, hintCord.C].transform))
        {
            board[hintCord.R, hintCord.C].image.sprite = gameLogic.WhosTurn == PawnType.X ? store.XSprite : store.Osprite;
            board[hintCord.R, hintCord.C].image.color = Color.white;

            board[hintCord.R, hintCord.C].transform.DOShakeRotation(1);

            yield return new WaitForSeconds(1);

            board[hintCord.R, hintCord.C].image.color = Color.clear;
            board[hintCord.R, hintCord.C].image.sprite = null;
        }
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

                board[r, c].onClick.AddListener(() => IsGameEnded(gameLogic.GameState));
                board[r, c].onClick.AddListener(() => StartCoroutine(TurnTimer(5)));
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
        while (!IsGameEnded(gameLogic.GameState))
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

    private void OnGameEnded(GameState state)
    {
        blackScreen.FadeBlackScreen(0.95f);
        SetBoardInteractive(false);
        hintButton.interactable = false;
        restartButton.interactable = false;
        undoButton.interactable = false;
        switch (state)
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

    private bool IsGameEnded(GameState state)
    {
        if (EnumUtils.IsGameEnded(state))
        {
            OnGameEnded(state);
            return true;
        }
        return false;
    }

    private IEnumerator AI_Turn()
    {
        isAIturn = true;
        SetBoardInteractive(false);
        yield return new WaitForSeconds(1);
        if (!EnumUtils.IsGameEnded(gameLogic.GameState))
        {
            Coordinate AI_move = new Coordinate(gameLogic.Hint());
            gameLogic.ConcludeTurn(AI_move);
            UpdateCell(AI_move);
            gameLogic.ChangeTurn();
            UpdateWhosTurn();
            if (!IsGameEnded(gameLogic.GameState))
                SetBoardInteractive(true);
        }
        isAIturn = false;
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

    private IEnumerator TurnTimer(int secsPerTurn)
    {
        if (!EnumUtils.IsGameEnded(gameLogic.GameState))
        {
            PawnType currentTurn = gameLogic.WhosTurn;
            float timer = secsPerTurn + 1;
            timerText.text = ((int)timer).ToString();
            while (currentTurn == gameLogic.WhosTurn)
            {
                timer -= Time.deltaTime;
                timerText.text = ((int)timer).ToString();
                if (timer <= 0)
                {
                    OnGameEnded(currentTurn == PawnType.X ? GameState.WinnerO : GameState.WinnerX);
                    SetBoardInteractive(false);
                    break;
                }

                yield return null;
            }
        }
    }

    private IEnumerator CountDown(int countDown, Action action)
    {
        blackScreen.FadeBlackScreen(0.8f);
        countdownText.gameObject.SetActive(true);
        float timer = countDown + 1;
        countdownText.text = ((int)timer).ToString();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            countdownText.text = ((int)timer).ToString();
            if (timer <= 0)
            {
                blackScreen.FadeBlackScreen(0f);
                countdownText.gameObject.SetActive(false);
                action();
                break;
            }

            yield return null;
        }
    }
}
