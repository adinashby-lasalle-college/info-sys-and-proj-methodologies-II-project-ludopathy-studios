

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
            BingoCage.OnBallDrawn += MarkNumber;
        }

        void OnDestroy()
        {
            BingoCage.OnBallDrawn -= MarkNumber;
        }
        private void Start()
        {
            PlayerSetup();
        }

        public void MarkNumber(int ball)
        {
            int bingoCardIndex = Numbers.IndexOf(ball);

            if (bingoCardIndex != -1)
            {
                GameManager.Instance.tornado.Power();
                MarkedSpace[bingoCardIndex] = 1;
                TableBtns[bingoCardIndex].interactable = false;
                ScoreManager.Instance.IncreasePlayerPoints(50);
            }
            else
            {
                Debug.Log(ball + " is not on bingoCard");
            }

        }

        public void ReturnBall(int ball)
        {
            int bingoCardIndex = Numbers.IndexOf(ball);

            if (bingoCardIndex != -1)
            {
                MarkedSpace[bingoCardIndex] = 0;
                TableBtns[bingoCardIndex].interactable = true;
                ScoreManager.Instance.DecreasePlayerPoints(50);
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
    }
}