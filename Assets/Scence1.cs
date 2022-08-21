using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scence1 : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameObject.Find("ArcherPlayer").SetActive(false);
            GameObject.Find("PlayerSoldier").SetActive(true);
        }
    }
}
