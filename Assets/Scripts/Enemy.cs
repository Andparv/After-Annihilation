using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GridLayout gridLayout;
    public List<Vector3Int> availableMoves;
    public Vector3Int pos;
    private bool yCoordOdd;

    public int health;
    public int strenght;
    public int maxEnergy = 1;
    public int energy;

    // Start is called before the first frame update
    void Awake()
    {
        gridLayout = GameObject.FindWithTag("grid").GetComponent<GridLayout>();
        health = 2;
        strenght = 3;
    }

    public void ResetEnergy()
    {
        energy = maxEnergy;
    }

    public void Move()
    {
        getAvailableMoves();
        int i = Random.Range(0, availableMoves.Count);
        Vector3Int cellCoords = availableMoves[i];
        if (Mathf.Abs(cellCoords.y) % 2 == 1) //cells with odd y coord
        {
            transform.position = new Vector3(1.2f * cellCoords.x + 0.6f, cellCoords.y + Mathf.Abs(cellCoords.y * 0.05f), 0);
        }
        else
        {
            transform.position = new Vector3(1.2f * cellCoords.x, cellCoords.y + Mathf.Abs(cellCoords.y * 0.05f), 0);
        }

        energy -= 1;
    }

    public void getAvailableMoves()
    {
        Vector3Int pos = gridLayout.WorldToCell(transform.position);
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
                    if (yCoordOdd && x + (-1) * Mathf.Abs(y) > -2)
                    {
                        Vector3Int tileCoord = new Vector3Int(x, y, 0) + pos;
                        if (tileCoord.x <= 7 && tileCoord.x >= -8 & tileCoord.y <= 3 && tileCoord.y >= -3)
                        {
                            availableMoves.Add(tileCoord);
                        }

                    }
                    else if (!yCoordOdd && x + Mathf.Abs(y) < 2)
                    {
                        Vector3Int tileCoord = new Vector3Int(x, y, 0) + pos;
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
