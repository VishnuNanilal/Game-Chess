using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get{ return instance; }
    }

    public GameObject activePlaceHolder;  //The placeHolder from which we drag the piece is the current placeholder anytime.
    public PlaceHolderStats[,] placeHolderPositionArray = new PlaceHolderStats[8,8];
    

    public bool isWhitesTurn;
    public Image turnIndicator;

    public delegate void themeChangedDel();
    public event themeChangedDel themeChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        #region to take care of Array

        for (int i=0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            {
               placeHolderPositionArray[i, j] = gameObject.AddComponent<PlaceHolderStats>();
            }
        }

        for (int i = 0; i < 8; i++)
        {
            placeHolderPositionArray[i, 0].isOccupied =true;
            placeHolderPositionArray[i, 1].isOccupied =true;  
            placeHolderPositionArray[i, 6].isOccupied =true;
            placeHolderPositionArray[i, 7].isOccupied =true;
        }

        #endregion
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        isWhitesTurn = true;
        
    }
    public void SceneChanger(string sceneName)
    {
        AsyncOperation ao=SceneManager.LoadSceneAsync(sceneName);
        ao.completed += AssignRefForNewScene;
    }

    private void AssignRefForNewScene(AsyncOperation obj)
    {
        turnIndicator = GameObject.Find("TurnIndicator").GetComponent<Image>();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public bool CheckTurn(GameObject draggedPiece)
    {
        if (draggedPiece.GetComponent<PieceStats>().isWhite && GameController.Instance.isWhitesTurn)
            return true;
        else if (!draggedPiece.GetComponent<PieceStats>().isWhite && !GameController.Instance.isWhitesTurn)
            return true;
        else
            return false;
    }

    public void ChangeTheme()
    {
        themeChanged();
    }
}
