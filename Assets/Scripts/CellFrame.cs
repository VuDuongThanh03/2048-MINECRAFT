using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFrame : MonoBehaviour
{
    // Start is called before the first frame update
    public bool haveTile = false;
    public GameObject tileOfCell;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getNumberOfTile()
    {
        return tileOfCell.GetComponent<Tile>().number;
    }
}
