using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    const int TEAM_ATT = 0;
    const int TEAM_DEF = 1;

    private void Start()
    {
        int charachter = PlayerPrefs.GetInt("character");
        SetPlayerActive(charachter);
    }

    public void FinishGame(int winner)
    {
        StartCoroutine(waitFunction());
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + winner);
    }

    public void SetPlayerActive(int player)
    {
        if (player == 0)
        {
           GameObject.Find("ArcherPlayer").SetActive(true);
           GameObject.Find("PlayerSoldier").SetActive(false);
        }
        else
        {
            GameObject.Find("PlayerSoldier").SetActive(true);
            GameObject.Find("ArcherPlayer").SetActive(false);
        }
    }

    IEnumerator waitFunction()
    {
        yield return new WaitForSeconds(120);

    }
}
