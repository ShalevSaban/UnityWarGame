using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAttackers : MonoBehaviour
{
    const int DEFENDERS_SCENCE = 1;
    // const int NUM_OF_ATTACKERS = 3;
    public GameController controller;

    private int aliveCounter;


    private void Start()
    {
        aliveCounter = GameObject.FindGameObjectsWithTag("AttackPlayer").Length;
        Debug.Log("attackers:" + aliveCounter);

    }

    IEnumerator waitFunction()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(120);
        Debug.Log(Time.time);

    }


    public void decrementAliveCount()
    {
        aliveCounter--;
        Debug.Log("COUNTER:" + aliveCounter);
        if (aliveCounter == 0)
        {
            StartCoroutine(waitFunction());
            waitFunction();
            controller.FinishGame(DEFENDERS_SCENCE);
        }
    }
}
