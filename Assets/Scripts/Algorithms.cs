using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms : MonoBehaviour
{
    PieceStats pieceStats;
    PieceStats.PieceType pieceType;
    bool isDiagonal;
    bool isStraight;
    bool isSingleCellMove;
    bool isKnightMove;
    bool isPawn;
    public void Start()
    {
        isDiagonal = false;
        isStraight = false;
        isSingleCellMove = false;
        isKnightMove = false;
    }
    public bool MovementPossible(int PHXPos, int PHYPos, GameObject currentPiece, GameObject newPlaceHolder) //Passing the x,y positions of the PlaceHoolder to check if it's an available position.
    {
        int xPos = currentPiece.GetComponent<PieceStats>().currentXPos;
        int yPos = currentPiece.GetComponent<PieceStats>().currentYPos;
        int xDiff = PHXPos-xPos;
        int yDiff = PHYPos-yPos;
        int xAbsDiff = Math.Abs(xDiff);
        int yAbsDiff = Math.Abs(yDiff);

        pieceStats = currentPiece.GetComponent<PieceStats>();
        pieceType = pieceStats.pieceType;

        if(pieceType==PieceStats.PieceType.Pawn) //If Type is Pawn, we impliment a differnet Algo since Pawn Algo is different.
        { 
            if (PawnMoveCheck(PHXPos, PHYPos, xDiff, yDiff, currentPiece,newPlaceHolder))
                return true;
            else
                return false;
        }

        else if (AlgorithCalc(xAbsDiff, yAbsDiff, currentPiece))
        {
            ResetMoveType();

            if (pieceType == PieceStats.PieceType.Knight|| pieceType == PieceStats.PieceType.Pawn)
            {
                return true;
            }
            else
            {
                if (IsPathClear(xPos, yPos, xDiff, yDiff)) //If pieceType is not Knight or Pawn, we need to check if the path is clear.
                {
                    return true;
                }
                else
                    return false;
            }
        }
        else
            return false;
    }

    private bool PawnMoveCheck(int PHxPos, int PHyPos, int xDiff, int yDiff, GameObject currentPiece,GameObject newPlaceHolder)
    {
        if ((yDiff != 1 && yDiff != -1) || xDiff > 1 || xDiff < -1)
            return false;

        if (xDiff==0) //If moving straight up and not diagonal.
        {
            if (newPlaceHolder.GetComponent<PlaceHolderStats>().isOccupied)
                return false;                                                   //Pawn can't move straight up if it's occupied.

            if (!pieceStats.isWhite)                                            //If Pawn is white,it can't move up, if black, can't move down.
            {
                if (yDiff != 1)
                    return false;
                else
                    return true;
            }

            else
            {
                if (yDiff != -1)
                    return false;
                else
                    return true;
            }

        }
        else //If xDiff is 1 or -1 NOTE: xDiff can be -1, 0 or 1 only. Otherwise it will return false from the first condition check in this function.
        {
            if (!newPlaceHolder.GetComponent<PlaceHolderStats>().isOccupied) //Pawn cannot move diagonally unless it's replacing an Enemy piece.
                return false;

            if (!pieceStats.isWhite)    //Like previous case, we have to check if it's moving forward( ie. Black pawn pieces going up and white, down).
            {
                if (yDiff != 1)
                    return false;

                if (!newPlaceHolder.GetComponent<PlaceHolderStats>().occupiedPiece.GetComponent<PieceStats>().isWhite) //To Check if the diagonal cell is occupied by enemy or not. Returns false if it's occupied by a piece of same color.
                    return false;
                else
                    return true;
            }
            else
            {
                if (yDiff != -1)
                    return false;

                if (newPlaceHolder.GetComponent<PlaceHolderStats>().occupiedPiece.GetComponent<PieceStats>().isWhite)
                    return false;
                else
                    return true;
            }

        }

    }

    private bool AlgorithCalc(int xAbsDiff, int yAbsDiff,GameObject currentPiece)
    {
        pieceType= currentPiece.GetComponent<PieceStats>().pieceType;
        //TODO remove this part and lay out the algorithms for each dataType through switch.
        if (xAbsDiff == yAbsDiff)
            isDiagonal = true;
        if (xAbsDiff == 0 || yAbsDiff == 0)
            isStraight = true;
        if (xAbsDiff <= 1 && yAbsDiff <= 1)
            isSingleCellMove = true;
        if ((xAbsDiff == 2 && yAbsDiff == 1) || (yAbsDiff == 2 && xAbsDiff == 1))
            isKnightMove = true;

        switch(pieceType)
        {
            case PieceStats.PieceType.King:
                if (isSingleCellMove == true)
                    return true;
                else
                    return false;

            case PieceStats.PieceType.Queen:
                if (isSingleCellMove || isDiagonal || isStraight)
                    return true;
                else
                    return false;

            case PieceStats.PieceType.Bishop:
                if (isDiagonal)
                    return true;
                else
                    return false;

            case PieceStats.PieceType.Knight:
                if (isKnightMove)
                    return true;
                else
                    return false;


            case PieceStats.PieceType.Rook:
                if (isStraight)
                    return true;
                else
                    return false;

            default:
                return false;
                
        }
    }

    private bool IsPathClear(int xPos, int yPos, int xDiff, int yDiff) //Funtion to check if the path is clear for King, Queen, Bishop, Rock. Pawn got it's own algo and Knight can jump over pieces.
    {
        //The possible movements for the concerned pieces are either straight(xDiff=0 or yDiff=0) or diagonal( |xDiff|=|yDiff|).

        if (xDiff == 0) 
        {
            int sign = (yDiff < 0) ? -1 : 1;

            for (int i = 1; i <= Math.Abs(yDiff)-1; i++)    //The loops in this block looks from the last cell to the current cell-1 and see if there is any piece in any of the call on the way.
            {
                if (GameController.Instance.placeHolderPositionArray[0, yPos + i * sign].isOccupied)
                    return false;
            }
            return true;
        }

        else if (yDiff == 0)
        {
            int sign = (xDiff < 0) ? -1 : 1;

            for (int i = 1; i <= Math.Abs(xDiff)-1; i++)
            {
                if (GameController.Instance.placeHolderPositionArray[xPos + i * sign, yPos].isOccupied)
                    return false;
            }
            return true;
        }
        else if (Math.Abs(xDiff) == Math.Abs(yDiff))
        {
            int signx = (xDiff < 0) ? -1 : 1;
            int signy = (yDiff < 0) ? -1 : 1;

            for (int i = 1 ; i <= Math.Abs(xDiff)-1; i++)
            {
                if (GameController.Instance.placeHolderPositionArray[xPos + i*signx, yPos + i*signy].isOccupied)
                    return false;
            }
            return true;
        }
        else
            return false;
    }

    void ResetMoveType() //Refreshes the movement type.
    {
        isDiagonal = false;
        isStraight = false;
        isSingleCellMove = false;
        isKnightMove = false;
    }
}
