using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenMenu(int steps)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2-steps);

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}


