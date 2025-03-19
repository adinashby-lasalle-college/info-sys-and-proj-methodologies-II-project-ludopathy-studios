using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Andres_Scene_Scripts{
    
public class GridManager : MonoBehaviour
{
    public int Turn;
    public int TurnCount;

    public GameObject[] TurnIcon;

    public GameObject[] Player;

    public GameObject[] CanvasBlock;

    public GameObject[] BingoTxt;

    public string SelectedNum1;
    public string SelectedNum2;


    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        Turn = 0;
        TurnCount = 0;

        TurnIcon = GameObject.FindGameObjectsWithTag("Holder");
        // BingoTxt = GameObject.FindGameObjectsWithTag("Bingo");

        TurnIcon[0].SetActive(true);
        // CanvasBlock[0].SetActive(false);
        
    }

    public void GridButtons(int Number)
    {
        var Pl1 = Player[0].GetComponentInChildren<Generate_Numbers>();
        
        if (Turn == 0)
        {
            Turn = 1;
            TurnIcon[0].SetActive(false);
            CanvasBlock[0].SetActive(true);

            Pl1.MarkedSpace[Number] = 1;
            SelectedNum1 = Pl1.TxtBox[Number].text;
                    
            Pl1.BingoCondition();
        }
        else
        {
            Turn = 0;

            TurnIcon[0].SetActive(true);
            //TurnIcon[1].SetActive(false);

            CanvasBlock[0].SetActive(true);
            //CanvasBlock[1].SetActive(true);

             Pl1.MarkedSpace[Number] = 1;
             SelectedNum1 = Pl1.TxtBox[Number].text;

            int TboxLength = Pl1.TxtBox.Length;
            for (int i = 0; i < TboxLength; i++)
            {
                var Num = Pl1.TxtBox[i].text;
                if (SelectedNum2 == Num)
                {
                    Pl1.PlBtns[i].interactable = false;

                }
            }

        }
    }
}
}