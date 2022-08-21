using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{



    public void PlayGameArcher()
    {
        PlayerPrefs.SetInt("character", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGameSolider()
    {
        PlayerPrefs.SetInt("character", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }





    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
