using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GridLayout gridLayout;
    public int maxEnergy = 1;
    public int energy;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = gridLayout.WorldToCell(transform.position);
        energy = maxEnergy;
        GameManager.instance.AddEnemy(this);
    }


    public void ResetEnergy()
    {
        energy = maxEnergy;
    }

    public void Move()
    {
        if (energy == 0)
        {
            return;
        }

        int xMovement = Random.Range(-1, 1);
        int yMovement = Random.Range(-1, 1);
        Debug.Log("x move: " + xMovement);
        Debug.Log("y move: " + yMovement);
        Vector3Int cellCoords = gridLayout.WorldToCell(new Vector3(transform.position.x + xMovement, transform.position.y + yMovement, 0));
        if (Mathf.Abs(cellCoords.y) % 2 == 1) //cells with odd y coord
        {
            transform.position = new Vector3(1.2f * cellCoords.x + 0.5f, cellCoords.y, 0);
        }
        else
        {
            transform.position = new Vector3(1.2f * cellCoords.x, cellCoords.y, 0);
        }
        energy -= 1;
    }
}
