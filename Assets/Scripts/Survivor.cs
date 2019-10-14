using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{

    public GridLayout gridLayout;
    public int maxEnergy = 3;
    public int energy;
    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
        GameManager.instance.AddSurvivor(this);
    }

    // Update is called once per frame
    void Update()
    {
        //if it's not player's turn return
        if (!GameManager.instance.playersTurn)
        {
            return;
        }

        
        if (Input.GetMouseButtonDown(0) && energy>0)
        {
            //get the cell coord where you click
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellCoords = gridLayout.WorldToCell(mousePos);
            
            
            if (Mathf.Abs(cellCoords.y) % 2 == 1) //cells with odd y coord
            {
                transform.position = new Vector3(1.2f * cellCoords.x + 0.5f, cellCoords.y, 0);
            }
            else
            {
                transform.position = new Vector3(1.2f * cellCoords.x, cellCoords.y, 0);
            }

            //Debug.Log("mousepos: " + mousePos);
            //Debug.Log("cellCoords: " + cellCoords);

            //after every move -1 energy
            energy -= 1;
            
        }
        
    }

    public void ResetEnergy()
    {
        energy = maxEnergy;
    }
}
