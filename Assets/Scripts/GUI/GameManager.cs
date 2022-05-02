using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GraphicsStore store;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform gameGrid;
    [SerializeField] private Image bg;
    private Button[,] board = new Button[3, 3];

    private GameLogic gameLogic = new GameLogic();

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
        InitBoard();
    }

    private void InitBoard()
    {
        bg.sprite = store.BGsprite;

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                Coordinate cord = new Coordinate(r, c);
                board[r, c] = Instantiate(cellPrefab, gameGrid).GetComponent<Button>();
                board[r, c].onClick.AddListener(() => gameLogic.ConcludeTurn(cord));
                board[r, c].onClick.AddListener(() => UpdateCellGraphics(cord));
                board[r, c].onClick.AddListener(() => gameLogic.ChangeTurn());
            }
        }
    }

    private void UpdateCellGraphics(Coordinate coordinate)
    {
        switch (gameLogic.board[coordinate.R, coordinate.C])
        {
            case PawnType.None:
                board[coordinate.R, coordinate.C].image.sprite = null;
                return;
            case PawnType.X:
                board[coordinate.R, coordinate.C].image.sprite = store.XSprite;
                return;
            case PawnType.O:
                board[coordinate.R, coordinate.C].image.sprite = store.Osprite;
                return;
        }
    }
}
