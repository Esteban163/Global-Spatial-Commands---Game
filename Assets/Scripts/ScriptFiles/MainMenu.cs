using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // this load the game scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // this shut down the game
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
