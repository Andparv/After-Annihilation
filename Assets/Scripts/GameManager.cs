using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool playersTurn = true;

    private List<Survivor> survivors;
    private List<Enemy> enemies;


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
        enemies = new List<Enemy>();
    }

    private void Update()
    {
        if (playersTurn)
        {
            //players turn
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("enemys turn");
                StartEnemyTurn();
            }


        }else
        {
            //enemys turn
            Debug.Log("players turn");
            StartPlayersTurn();

            


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

    private void StartEnemyTurn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ResetEnergy();
            enemies[i].Move();

        }
        playersTurn = false;


    }

    public void AddSurvivor(Survivor survivor)
    {
        survivors.Add(survivor);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
}
