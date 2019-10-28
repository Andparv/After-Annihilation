using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Survivor : MonoBehaviour
{
    private Tilemap tilemap;
    private GridLayout gridLayout;
    public List<Vector3Int> availableMoves;
    public Vector3Int pos;
    private bool yCoordOdd;

    public int health;
    public int strenght;
    public int maxEnergy = 3;
    public int energy;
    public bool active;

    // Start is called before the first frame update
    void Awake()
    {
        gridLayout = GameObject.FindWithTag("grid").GetComponent<GridLayout>();
        tilemap = gridLayout.GetComponentInChildren<Tilemap>();
        active = false;
        pos = gridLayout.WorldToCell(transform.position);
        energy = maxEnergy;
        health = 5;
        strenght = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if it's not player's turn return
        if (!GameManager.instance.playersTurn)
        {
            return;
        }

        if (!active)
        {
            return;
        }


        if (Input.GetMouseButtonDown(0) && energy>0 && !EventSystem.current.IsPointerOverGameObject())
        {

            //get the cell coord where you click
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellCoords = gridLayout.WorldToCell(mousePos);
            if (availableMoves.Contains(cellCoords))
            {
                if (Mathf.Abs(cellCoords.y) % 2 == 1) //cells with odd y coord
                {
                    transform.position = new Vector3(1.2f * cellCoords.x + 0.6f, cellCoords.y + Mathf.Abs(cellCoords.y * 0.05f), 0);
                }
                else
                {
                    transform.position = new Vector3(1.2f * cellCoords.x, cellCoords.y + Mathf.Abs(cellCoords.y * 0.05f), 0);
                }
                HideAvailableMoves();
                pos = gridLayout.WorldToCell(transform.position);

                //after every move -1 energy
                energy -= 1;
                if (energy > 0)
                {
                    getAvailableMoves();
                    ShowAvailableMoves();
                }

            }

        }
        
    }

    public void ResetEnergy()
    {
        energy = maxEnergy;
        if (active)
        {
            getAvailableMoves();
            ShowAvailableMoves();
        }
    }

    public void SetActive(bool b)
    {
        active = b;
        if (b == true)
        {
            getAvailableMoves();
            ShowAvailableMoves();
        }
        else
        {
            HideAvailableMoves();
        }
    }

    public void ShowAvailableMoves()
    {
        foreach (Vector3Int tileCoord in availableMoves)
        {
            tilemap.SetTileFlags(tileCoord, TileFlags.None);
            tilemap.SetColor(tileCoord, new Color(1.0f, 0.9f, 0.02f, 0.8f));
        }        
    }

    public void HideAvailableMoves()
    {
        foreach (Vector3Int tileCoord in availableMoves)
        {
            tilemap.SetColor(tileCoord, new Color(1, 1, 1, 1));
        }
    }

    public void getAvailableMoves()
    {
        
        if (Mathf.Abs(pos.y) % 2 == 1)
        {
            yCoordOdd = true;
        }
        else
        {
            yCoordOdd = false;
        }
        availableMoves.Clear();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (yCoordOdd && x +(-1) * Mathf.Abs(y) > -2)
                    {
                        Vector3Int tileCoord = new Vector3Int(x, y, 0) + pos;
                        if (!GameManager.instance.GetSurvivorsLocations().Contains(tileCoord))
                        {
                            if (tileCoord.x <= 7 && tileCoord.x >= -8 & tileCoord.y <= 3 && tileCoord.y >= -3)
                            {
                                availableMoves.Add(tileCoord);
                            }
                        }
 
                    }
                    else if (!yCoordOdd && x + Mathf.Abs(y) < 2)
                    {
                        Vector3Int tileCoord = new Vector3Int(x, y, 0) + pos;
                        if (!GameManager.instance.GetSurvivorsLocations().Contains(tileCoord))
                        {
                            if (tileCoord.x <= 7 && tileCoord.x >= -8 & tileCoord.y <= 3 && tileCoord.y >= -3)
                            {
                                availableMoves.Add(tileCoord);
                            }
                        }
                    }
                    
                }
            }
        }
        
    }
}
