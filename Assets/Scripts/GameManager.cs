using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Andres_Scene_Scripts{
    
public class GameManager : MonoBehaviour
{
    public int Turn;
    public int TurnCount;

    public GameObject[] TurnIcon;

    public GameObject[] Player;

    public GameObject[] CanvasBlock;

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

        TurnIcon[0].SetActive(true);
        TurnIcon[1].SetActive(false);

        CanvasBlock[0].SetActive(false);
        CanvasBlock[1].SetActive(true);
    }

    public void GridButtons(int Number)
    {
        var Pl1 = Player[0].GetComponentInChildren<Generate_Numbers>();
        // var Pl2 = Player[1].GetComponentInChildren<Generate_Numbers>();

        //Change Player on Button Click - 0 - player 1, 1 - player 2 
        if (Turn == 0)
        {
            Turn = 1;
            TurnIcon[0].SetActive(false);
            //TurnIcon[1].SetActive(true);

            CanvasBlock[0].SetActive(true);
            //CanvasBlock[1].SetActive(false);

            Pl1.MarkedSpace[Number] = 1;
            SelectedNum1 = Pl1.TxtBox[Number].text;
            
            // int TboxLength = Pl2.TxtBox.Length;
            // for (int i = 0; i < TboxLength; i++)
            // {
            //     var Num = Pl2.TxtBox[i].text;
            //     if (SelectedNum1 == Num)
            //     {
            //         Pl2.PlBtns[i].interactable = false;

            //         Pl2.MarkedSpace[i] = 1;
            //     }
            // }           

            Pl1.WinCondition();
        }
        else
        {
            Turn = 0;

            TurnIcon[0].SetActive(true);
            //TurnIcon[1].SetActive(false);

            CanvasBlock[0].SetActive(false);
            //CanvasBlock[1].SetActive(true);

            // Pl2.MarkedSpace[Number] = 1;
            // SelectedNum2 = Pl2.TxtBox[Number].text;

            int TboxLength = Pl1.TxtBox.Length;
            for (int i = 0; i < TboxLength; i++)
            {
                var Num = Pl1.TxtBox[i].text;
                if (SelectedNum2 == Num)
                {
                    Pl1.PlBtns[i].interactable = false;

                    Pl1.MarkedSpace[i] = 1;
                }
            }

            // Pl2.WinCondition();
        }
    }
}
}