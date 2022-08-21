using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeamDefenders : MonoBehaviour
{
    const int ATTACKERS_SCENCE = 2;
    //const int NUM_OF_DEFENDERS = 1;
    public GameController controller;

    private int aliveCounter;

    private void Start()
    {
       aliveCounter= GameObject.FindGameObjectsWithTag("DeffendPlayer").Length;
        Debug.Log("deffenders:" + aliveCounter);
    }


    public void decrementAliveCount()
    {
        aliveCounter--;
        Debug.Log("COUNTER:" + aliveCounter);
        if (aliveCounter == 0)
        {
            StartCoroutine(waitFunction());
            waitFunction();
            controller.FinishGame(ATTACKERS_SCENCE);

        }

    }

    IEnumerator waitFunction()
    {
        Debug.Log(Time.time);

        yield return new WaitForSeconds(120);
        Debug.Log(Time.time);
    }
}
