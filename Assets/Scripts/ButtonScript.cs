using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Level1";
    [SerializeField] private string titleScreen = "TitleScene";
    [SerializeField] private string controlScreen = "ControlScene";
    
    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I QUIT!");
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void ControlScreen()
    {
        SceneManager.LoadScene(controlScreen);
    }
}
