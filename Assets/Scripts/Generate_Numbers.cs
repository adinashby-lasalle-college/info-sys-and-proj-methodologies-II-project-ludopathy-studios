using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

namespace Andres_Scene_Scripts{
    public class Generate_Numbers : MonoBehaviour
    {
        public TMP_Text[] TxtBox; 
        public List<int> Numbers = new List<int>();
        public Button[] PlBtns;
        public GridManager gridManager;
        public int[] MarkedSpace;
        public GameObject[] BingoTxt;
        public static int[,] GenerateBingoCard;

        // Start is called before the first frame update
        private void Start()
        {
            PlayerSetup();
        }

        void PlayerSetup()
        {
            int[,] Card = BingoCardGenerator.GenerateBingoCard();

            int col= 0;
            int row = 0;
            
            BingoTxt = GameObject.FindGameObjectsWithTag("Bingo");

            // Assigning number to button text on the grid
            for (int j = 0; j < TxtBox.Length; j++)
            {
                if(j % 5 == 0 && j != 0)
                {
                    row++;
                    col = 0;
                }
                TxtBox[j].text = Card[row,col].ToString();
                Numbers.Add(Card[row,col]);
                col++;
            }

            gridManager = FindObjectOfType<GridManager>();

            //Set Marked Spaces value to zero
            for (int i = 0; i < MarkedSpace.Length; i++)
            {
                MarkedSpace[i] = 0;
            }
        }

        public void SetBtnUninteractable(int Number)
        {
            PlBtns[Number].interactable = false;
        }

        public bool BingoCondition()
        {
            int[] winConditions = new int[]
            {
                //Horizontal
            MarkedSpace[0] + MarkedSpace[1] + MarkedSpace[2] + MarkedSpace[3] + MarkedSpace[4],
            MarkedSpace[5] + MarkedSpace[6] + MarkedSpace[7] + MarkedSpace[8] + MarkedSpace[9],
            MarkedSpace[10] + MarkedSpace[11] + MarkedSpace[12] + MarkedSpace[13] + MarkedSpace[14],
            MarkedSpace[15] + MarkedSpace[16] + MarkedSpace[17] + MarkedSpace[18] + MarkedSpace[19],
            MarkedSpace[20] + MarkedSpace[21] + MarkedSpace[22] + MarkedSpace[23] + MarkedSpace[24],
            
            //Vertical
            MarkedSpace[0] + MarkedSpace[5] + MarkedSpace[10] + MarkedSpace[15] + MarkedSpace[20],
            MarkedSpace[1] + MarkedSpace[6] + MarkedSpace[11] + MarkedSpace[16] + MarkedSpace[21],
            MarkedSpace[2] + MarkedSpace[7] + MarkedSpace[12] + MarkedSpace[17] + MarkedSpace[22],
            MarkedSpace[3] + MarkedSpace[8] + MarkedSpace[13] + MarkedSpace[18] + MarkedSpace[23],
            MarkedSpace[4] + MarkedSpace[9] + MarkedSpace[14] + MarkedSpace[19] + MarkedSpace[24],

            //Diagonal
            MarkedSpace[0] + MarkedSpace[6] + MarkedSpace[12] + MarkedSpace[18] + MarkedSpace[24],
            MarkedSpace[4] + MarkedSpace[8] + MarkedSpace[12] + MarkedSpace[16] + MarkedSpace[20]   

            };
            
            // var solution = new int[] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12};

                    foreach (int sum in winConditions)
            {
                Debug.Log("Checking sum: " + sum);
                if (sum == 5)
                    Debug.Log("Bingo");
                    //BingoTxt[0].SetActive(true); 
                    return true; // Player 1 wins
            }

            return false; // No win yet
        }
            // int SumOfMarkedSpace = -1;

            // foreach(var sol in solution)
            // {
            //     if(sol == 5)
            //     {
            //         //Debug.Log(gameObject.name + " " );
            //         //BingoTxt[].SetActive(true);
            //     }
            
        
    }
}