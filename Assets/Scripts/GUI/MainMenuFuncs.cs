using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuFuncs : MonoBehaviour
{
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void DisableButton(Button button)
    {
        button.interactable = false;
    }
    public void EnableButton(Button button)
    {
        button.interactable = true;
    }
}
