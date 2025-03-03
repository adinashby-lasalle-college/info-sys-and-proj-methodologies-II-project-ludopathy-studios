

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Andres_Scene_Scripts
{
    public class BingoCard : Singleton<BingoCard>
    {
        public TMP_Text[] TxtBox;
        public List<int> Numbers = new List<int>();
        public Button[] TableBtns;
        public int[] MarkedSpace;
        public GameObject[] BingoTxt;

        protected override void Awake()
        {
            base.Awake();
            BingoCage.OnBallDrawn += OnBallDrawn;
        }

        void OnDestroy()
        {
            BingoCage.OnBallDrawn -= OnBallDrawn;
        }
        private void Start()
        {
            PlayerSetup();
        }

        void OnBallDrawn(int ball)
        {
            int bingoCardIndex = Numbers.IndexOf(ball);
            Debug.Log(bingoCardIndex);
            if (bingoCardIndex != -1)
            {
                MarkedSpace[bingoCardIndex] = 1;
                TableBtns[bingoCardIndex].interactable = false;
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
    }
}