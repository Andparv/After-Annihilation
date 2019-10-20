using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool playersTurn;
    private int currentlyActive;
    private bool clickingOnUI;

    public GameObject survivor;
    private List<Survivor> survivors;
    private List<Enemy> enemies;

    public GameObject charButtonPrefab;
    private GameObject charButton;
    private GameObject characterUI;


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
    }

    private void Start()
    {
        survivors = new List<Survivor>();
        enemies = new List<Enemy>();

        characterUI = GameObject.FindWithTag("CharacterUI");
        SpawnCharater(survivor,"survivor", new Vector3(0, 0, 0));
        currentlyActive = 0;
        SpawnCharater(survivor, "survivor", new Vector3(0, 0, 0));
        playersTurn = true;
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
            StartEnemyTurn();
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
            Debug.Log("survivor" + i + ": Energy(" + survivors[i].energy + "), survivorAct1ve(" + survivors[i].active + ")");
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


    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");
    }
    //adding characters to list
    public void AddSurvivor(Survivor survivor)
    {
        survivors.Add(survivor);
        charButton = Instantiate(charButtonPrefab, characterUI.transform);
        Button btn = charButton.GetComponentInChildren<Button>();
        btn.onClick.AddListener(() => SelectCharacter(survivor));
        
        charButton.transform.position = new Vector3(30, survivors.Count *  70, 0);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    //spawning characters
    public void SpawnCharater(GameObject c, string type, Vector3 location)
    {
        if (type == "survivor")
        {
            Survivor survivor = Instantiate(c.GetComponent<Survivor>(), location, Quaternion.identity);
            AddSurvivor(survivor);
        }
        if (type == "enemy")
        {
            Enemy enemy = Instantiate(c.GetComponent<Enemy>(), location, Quaternion.identity);
            AddEnemy(enemy);
        }
        
    }

    public void SelectCharacter(Survivor survivor)
    {
        Debug.Log("button clicked ");
        survivors[currentlyActive].SetActive(false);
        survivor.SetActive(true);
    }
}
