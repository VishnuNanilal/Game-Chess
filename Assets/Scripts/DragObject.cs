using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Handles gameObject getting dragged
public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public RayCastHandler rayCastHandler;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    GameObject[] pieces;
    PieceStats pieceStats;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        pieceStats = GetComponent<PieceStats>();
        canvasGroup = GetComponent<CanvasGroup>();
        pieces = GameObject.FindGameObjectsWithTag("Piece");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetPreviousPlaceHolderBeforePicking(eventData.pointerDrag);
        if (GameController.Instance.CheckTurn(eventData.pointerDrag))
        {
            foreach (GameObject piece in pieces)
            {
                piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (GameController.Instance.CheckTurn(eventData.pointerDrag))
        {
            rectTransform.anchoredPosition += eventData.delta;
            canvasGroup.alpha = .75f;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (GameObject piece in pieces)
        {
            piece.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        canvasGroup.alpha = 1;
    }   

    void SetPreviousPlaceHolderBeforePicking(GameObject pointerDrag)
    {
        GameController.Instance.activePlaceHolder = pointerDrag.GetComponent<PieceStats>().PlaceHolderOfThisPiece;
    }

}
