using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMode : MonoBehaviour
{
    [SerializeField] ModeData modeData;
    [SerializeField] Button setModeButton;
    [SerializeField] Toggle playerVsplayer;
    [SerializeField] Toggle computerVsComputer;
    [SerializeField] TMP_InputField player1Name;
    [SerializeField] TMP_InputField player2Name;
    private void Start()
    {
        UpdateNecccessaryFields();
    }

    public void SetMode()
    {
        string p1Name = GetGameMode() == GameMode.computerVScomputer ? "Player1_AI" : player2Name.text;
        string p2Name = GetGameMode() == GameMode.playerVsplayer ? player2Name.text : "Player2_AI";
        modeData.SetData(GetGameMode(), p1Name == "" ? "Player1" : p1Name, p2Name == "" ? "Player2" : p2Name);
    }

    public void UpdateNecccessaryFields()
    {
        if (GetGameMode() == GameMode.playerVsplayer)
        {
            player1Name.gameObject.SetActive(true);
            player2Name.gameObject.SetActive(true);
        }
        else if (GetGameMode() == GameMode.playerVScomputer)
        {
            player1Name.gameObject.SetActive(true);
            player2Name.gameObject.SetActive(false);
        }
        else
        {
            player1Name.gameObject.SetActive(false);
            player2Name.gameObject.SetActive(false);
        }
    }
    private GameMode GetGameMode()
    {
        if (playerVsplayer.isOn)
        {
            return GameMode.playerVsplayer;
        }
        else if (computerVsComputer.isOn)
        {
            return GameMode.computerVScomputer;
        }
        else
        {
            return GameMode.playerVScomputer;
        }
    }
}
