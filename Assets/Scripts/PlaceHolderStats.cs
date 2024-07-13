using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderStats : MonoBehaviour
{
    public bool isOccupied;
    public GameObject occupiedPiece=null;
    public int xPos;  //PlaceHolder x and y values with origin at bottom left corner.
    public int yPos;

    public PlaceHolderStats()
    {
        this.isOccupied = false;
    }
}
