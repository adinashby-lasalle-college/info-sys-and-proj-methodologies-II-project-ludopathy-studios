using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Andres_Scene_Scripts
{
    public class BingoCard : Singleton<BingoCard>
    {
        public TMP_Text[] TxtBox;
        public List<int> Numbers = new List<int>();
        public Button[] TableBtns;
        public int[] MarkedSpace;
        public GameObject[] BingoTxt;
        private PowerUpBase[] PowerUpGrid; 
        private bool[] BombGrid;  

        protected override void Awake()
        {
            base.Awake();
            BingoCage.OnBallDrawn += MarkNumber;
        }

        void OnDestroy()
        {
            BingoCage.OnBallDrawn -= MarkNumber;
        }
        private void Start()
        {
            PlayerSetup();
            PowerUpGrid = new PowerUpBase[25];
            BombGrid = new bool[25]; 
        }

        void MarkNumber(int ball)
        {
            int bingoCardIndex = Numbers.IndexOf(ball);

            if (bingoCardIndex != -1)
            {
                MarkedSpace[bingoCardIndex] = 1;
                TableBtns[bingoCardIndex].interactable = false;
            }
            else
            {
                Debug.Log(ball + " is not on bingoCard");
            }

        }

        void PlayerSetup()
        {
            int[,] Card = BingoCardGenerator.GenerateBingoCard();

            int col = 0;
            int row = 0;

            // Assigning number to button text on the grid
            for (int j = 0; j < TxtBox.Length; j++)
            {
                if (j % 5 == 0 && j != 0)
                {
                    row++;
                    col = 0;
                }
                TxtBox[j].text = Card[row, col].ToString();
                Numbers.Add(Card[row, col]);
                col++;
            }

            //Set Marked Spaces value to zero
            for (int i = 0; i < MarkedSpace.Length; i++)
            {
                MarkedSpace[i] = 0;
            }
        }

        public bool isBingo()
        {
            for (int i = 0; i < MarkedSpace.Length; i++)
            {
                if (MarkedSpace[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveNumbersInRow(int rowIndex)
        {
                int rowStart = rowIndex * 5;
                for (int i = 0; i < 5; i++)
            {
                int cellIndex = rowStart + i;
                if (MarkedSpace[cellIndex] == 1)
                {
                    int numberToReturn = Numbers[cellIndex];
                    MarkedSpace[cellIndex] = 0;
                    TableBtns[cellIndex].interactable = true;
                    BingoCage.Instance.ReturnNumberToCage(numberToReturn);
                    Debug.Log($"Twister: Number {numberToReturn} returned to Bingo Cage");
                }
            }
        }

        public void TriggerColumnPowerUps(int columnIndex)
        {
            for (int i = 0; i < 5; i++)
            {
                int cellIndex = columnIndex + (i * 5);
                if (PowerUpGrid[cellIndex] != null) // Assuming PowerUpGrid stores power-ups
                {
                    if (PowerUpGrid[cellIndex] is TwisterPowerUp) 
                    {
                        Debug.Log("Chain Reaction stopped by Twister!");
                        RemoveNumbersInRow(cellIndex / 5);
                        return; // End chain reaction
                    }
                    else
                    {
                        PowerUpGrid[cellIndex].Activate();
                    }
                }
            }   
        }

        public void DrawRowAndColumnNumbers(int cellIndex)
        {
                int rowIndex = cellIndex / 5;
                int colIndex = cellIndex % 5;

                // Check if there is a bomb in the selected area
                if (BombGrid[cellIndex]) // Assuming BombGrid stores bomb locations
                {
                    Debug.Log("Bomberman hit a bomb! Ability canceled.");
                    return;
                }

                // Draw numbers in the row
                for (int i = 0; i < 5; i++)
                {
                    int rowCell = (rowIndex * 5) + i;
                    if (MarkedSpace[rowCell] == 0) 
                    {
                        BingoCage.Instance.ChooseNextNumber(Numbers[rowCell]);
                    }
                }

                // Draw numbers in the column
                for (int i = 0; i < 5; i++)
                {
                    int colCell = colIndex + (i * 5);
                    if (MarkedSpace[colCell] == 0)
                    {
                        BingoCage.Instance.ChooseNextNumber(Numbers[colCell]);
                    }
                }
        }
    public class TwisterPowerUp : PowerUpBase
    {
        private int cellIndex; // The index where Twister is placed

        public TwisterPowerUp(int cellIndex)
        {
            this.cellIndex = cellIndex;
        }

        public new virtual void Activate()
        {
            int rowIndex = cellIndex / 5;

            Debug.Log($"Twister activated on row {rowIndex}!");
            Instance.RemoveNumbersInRow(rowIndex);
        }
    }

    public class PowerUpBase
    {
        public virtual void Activate()
        {
            Debug.Log("Power-up activated! (Default behavior)");
        }
    }
    }
}