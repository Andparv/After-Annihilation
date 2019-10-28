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
    private int spawnTurn;

    public GameObject survivor;
    public GameObject enemy;
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
        spawnTurn = 0;
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
                StartEnemyTurn();
                Debug.Log("players turn");
            }

        }
    }

    private void StartPlayersTurn()
    {
        //restore energy of all survivors
        Debug.Log(GetSurvivorsLocations()[0]);
        Debug.Log(GetSurvivorsLocations()[1]);
        for (int i = 0; i < survivors.Count; i++)
        {
            survivors[i].ResetEnergy();
        }

        playersTurn = true;
    }

    private void StartEnemyTurn()
    {
        if (enemies.Count < 5)
        {
            if (spawnTurn == 0)
            {
                SpawnCharater(enemy, "enemy", new Vector3(-6, 2.2f, 0));
                spawnTurn = 3;
            }
            else
            {
                spawnTurn -= 1;
            }
            
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ResetEnergy();
            enemies[i].Move();

        }


        StartPlayersTurn();
    }

    //adding characters to list
    public void AddSurvivor(Survivor survivor)
    {
        survivors.Add(survivor);
        charButton = Instantiate(charButtonPrefab, characterUI.transform);
        Button btn = charButton.GetComponentInChildren<Button>();
        int addedPos = survivors.Count-1;
        btn.onClick.AddListener(() => SelectCharacter(survivor, addedPos));
        
        charButton.transform.position = new Vector3(20, (survivors.Count+ 1) *  70, 0);
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

    public void SelectCharacter(Survivor survivor,int position)
    {
        survivors[currentlyActive].SetActive(false);
        SpriteRenderer currentlyActiveSRenderer = survivors[currentlyActive].GetComponentsInChildren<SpriteRenderer>()[1];
        currentlyActiveSRenderer.color = new Color(1, 1, 1, 0);

        survivor.SetActive(true);
        SpriteRenderer newActiveSRenderer = survivor.GetComponentsInChildren<SpriteRenderer>()[1];
        newActiveSRenderer.color = new Color(1, 1, 1, 1);

        currentlyActive = position;

    }
    public List<Vector3Int> GetSurvivorsLocations()
    {
        List<Vector3Int> survivorsLocations = new List<Vector3Int>();
        foreach (Survivor s in survivors)
        {
            survivorsLocations.Add(s.pos);
        }
        return survivorsLocations;
    }

    public List<Vector3Int> GetEnemiesLocations()
    {
        List<Vector3Int> enemiesLocations = new List<Vector3Int>();
        foreach (Enemy e in enemies)
        {
            enemiesLocations.Add(e.pos);
        }
        return enemiesLocations;
    }
}
