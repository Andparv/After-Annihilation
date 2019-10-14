using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool playersTurn = true;

    private List<Survivor> survivors;


    void Awake()
    {
        if (instance == null)
        {

            instance = this;

        }
        else if(instance != this){

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);

        survivors = new List<Survivor>();
    }

    private void Update()
    {
        if (playersTurn)
        {
            //players turn
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("enemys turn");
                playersTurn = false;
            }


        }else
        {
            //enemys turn

            if (Input.GetKeyDown("q"))
            {
                Debug.Log("players turn");
                StartPlayersTurn();
            }

        }
    }

    private void StartPlayersTurn()
    {
        //restore energy of all survivors
        for (int i = 0; i < survivors.Count; i++)
        {
            survivors[i].ResetEnergy();
        }
        playersTurn = true;
    }

    public void AddSurvivor(Survivor survivor)
    {
        survivors.Add(survivor);
    }
}
