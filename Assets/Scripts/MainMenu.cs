using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // PlayMaze Loads the maze scene when the Play button is pressed.
    public void PlayMaze()
    {
        SceneManager.LoadScene("maze");
    }
    // QuitMaze quits the game.
    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
