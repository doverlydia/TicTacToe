using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SelectMode : MonoBehaviour
{
    [SerializeField] ModeData modeData;
    [SerializeField] Button setModeButton;
    [SerializeField] Toggle playerVsplayer;
    [SerializeField] Toggle computerVsComputer;
    [SerializeField] TMP_InputField player1Name;
    [SerializeField] TMP_InputField player2Name;
    [SerializeField] private TMP_Dropdown dropdown;

    private void OnEnable()
    {
        UpdateNecccessaryFields();
        PopulateDropDownWithEnum(dropdown, modeData.difficulty);
    }

    private void PopulateDropDownWithEnum(TMP_Dropdown dropdown, Enum targetEnum)
    {
        Type enumType = targetEnum.GetType();
        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < Enum.GetNames(enumType).Length; i++)
        {
            newOptions.Add(new TMP_Dropdown.OptionData(Enum.GetName(enumType, i)));
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(newOptions);
    }

    public void SetMode()
    {
        string p1Name = GetGameMode() == GameMode.computerVScomputer ? "Player1_AI" : player1Name.text;
        string p2Name = GetGameMode() == GameMode.playerVsplayer ? player2Name.text : "Player2_AI";
        modeData.SetData(GetGameMode(), GetGameDifficulty(), p1Name == "" ? "Player1" : p1Name, p2Name == "" ? "Player2" : p2Name);
    }

    public void UpdateNecccessaryFields()
    {
        if (GetGameMode() == GameMode.playerVsplayer)
        {
            player1Name.gameObject.SetActive(true);
            player2Name.gameObject.SetActive(true);
            dropdown.gameObject.SetActive(false);
        }
        else if (GetGameMode() == GameMode.playerVScomputer)
        {
            player1Name.gameObject.SetActive(true);
            player2Name.gameObject.SetActive(false);
            dropdown.gameObject.SetActive(true);
        }
        else
        {
            player1Name.gameObject.SetActive(false);
            player2Name.gameObject.SetActive(false);
            dropdown.gameObject.SetActive(false);
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
    private GameDifficulty GetGameDifficulty()
    {
        switch (dropdown.options[dropdown.value].text)
        {
            case "easy":
                return GameDifficulty.easy;
            case "hard":
                return GameDifficulty.hard;
            default:
                return GameDifficulty.normal;
        }
    }
}
