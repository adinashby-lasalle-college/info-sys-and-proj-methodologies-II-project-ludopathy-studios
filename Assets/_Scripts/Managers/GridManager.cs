using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

namespace Andres_Scene_Scripts
{

    public class GridManager : Singleton<GridManager>
    {

        BingoCard bingoCardUI;
        public GameObject[] BallDrawnGO;
        public GameObject[] CanvasBlock;

        // Start is called before the first frame update
        void Start()
        {
            GameSetup();
            bingoCardUI = BingoCard.Instance;
            BallDrawnGO = GameObject.FindGameObjectsWithTag("Holder");
        }

        void GameSetup()
        {
            Debug.Log("Game Start");
        }

        public void OnClickCell(int Number)
        {
            bingoCardUI.MarkedSpace[Number] = 1;
            bingoCardUI.TableBtns[Number].interactable = false;
        }
    }
}