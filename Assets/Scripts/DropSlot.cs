using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Calls Event when an object is places in the gameObject.
public class DropSlot : MonoBehaviour, IDropHandler
{   
    Algorithms algo;
    PlaceHolderStats placeHolderStats;

    void Start()
    {
        placeHolderStats = GetComponent<PlaceHolderStats>();
        algo = GameObject.Find("SceneManager").GetComponent<Algorithms>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        bool isPieceDropped = false;

        if (eventData != null)
        {
            if (IsOccupiable(eventData.pointerDrag)) // TO check if the cell is empty or occupied by the opposite colored piece. If not returns false.
            {
                if (algo.MovementPossible(placeHolderStats.xPos, placeHolderStats.yPos,eventData.pointerDrag, gameObject)) //Check if the move is allowed for the current piece.
                {
                    if (!GameController.Instance.CheckTurn(eventData.pointerDrag))
                        return;

                    isPieceDropped = true;

                    PlacePiece(eventData.pointerDrag);
                    UnOccupyPrevPlaceHolder();

                    GameController.Instance.isWhitesTurn = !GameController.Instance.isWhitesTurn;

                    if (GameController.Instance.isWhitesTurn)
                        GameController.Instance.turnIndicator.color = Color.white;
                    else
                        GameController.Instance.turnIndicator.color = Color.black;
                }

            }
        }
        if (!isPieceDropped)
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GameController.Instance.activePlaceHolder.GetComponent<RectTransform>().anchoredPosition;

    }

    public bool IsOccupiable(GameObject draggedPiece)
    {
        if (!placeHolderStats.isOccupied)
            return true;
        if ((draggedPiece.GetComponent<PieceStats>().isWhite && !placeHolderStats.occupiedPiece.GetComponent<PieceStats>().isWhite) || (!draggedPiece.GetComponent<PieceStats>().isWhite && placeHolderStats.occupiedPiece.GetComponent<PieceStats>().isWhite))
            return true;
        else
            return false;
    }

    void PlacePiece(GameObject draggedPiece)
    {
        if (placeHolderStats.isOccupied)
        {
            placeHolderStats.occupiedPiece.SetActive(false);
        }

        draggedPiece.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;       //Set Anchor position to that of the new cell
        draggedPiece.GetComponent<PieceStats>().SetNewPos(placeHolderStats.xPos, placeHolderStats.yPos);                    //passes PH's x and y pos to be set as the current object's x and y 
     
        placeHolderStats.occupiedPiece = draggedPiece;                                                                      //Set dragged piece as piece of this cell.
        draggedPiece.GetComponent<PieceStats>().PlaceHolderOfThisPiece = gameObject;                                        //Set this PH as the PH of the dragged piece.

        placeHolderStats.isOccupied = true;
        GameController.Instance.placeHolderPositionArray[placeHolderStats.xPos, placeHolderStats.yPos].isOccupied = true;
    }
    void UnOccupyPrevPlaceHolder()
    {
        PlaceHolderStats prevPlaceHolder = GameController.Instance.activePlaceHolder.GetComponent<PlaceHolderStats>();

        prevPlaceHolder.occupiedPiece = null;
        prevPlaceHolder.isOccupied = false;
        GameController.Instance.placeHolderPositionArray[prevPlaceHolder.xPos, prevPlaceHolder.yPos].isOccupied = false;
    }

}
