

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BingoCard : Singleton<BingoCard>
{
    public TMP_Text[] TxtBox;
    public List<int> PlayerNumbers = new List<int>();
    public Button[] BingoCardBtns;
    public int[] MarkedSpace;
    public List<Cell> Cells = new List<Cell>();
    void OnEnable()
    {
        BingoCage.OnBallDrawn += MarkNumber;
    }
    void OnDisable()
    {
        BingoCage.OnBallDrawn -= MarkNumber;
    }
    private void Start()
    {
        PlayerSetup();
    }

    public void MarkNumber(int ballNumber)
    {
        int bingoCardIndex = PlayerNumbers.IndexOf(ballNumber);

        // Verifies if the number is in the Player Bingo Card and Modifies the button accordingly
        if (bingoCardIndex != -1)
        {
            MarkedSpace[bingoCardIndex] = 1;
            Debug.Log(bingoCardIndex);
            BingoCardBtns[bingoCardIndex].interactable = false;
            ScoreManager.Instance.IncreasePlayerPoints(50);
        }
        else
        {
            Debug.Log(ballNumber + " is not on bingoCard");
        }
    }

    public void UnmarkNumber(int ballNumber)
    {
        int bingoCardIndex = PlayerNumbers.IndexOf(ballNumber);

        // Unmark a selected number making sure to return the balls to Bingo Cage 
        if (bingoCardIndex != -1)
        {
            MarkedSpace[bingoCardIndex] = 0;
            BingoCardBtns[bingoCardIndex].interactable = true;
            ScoreManager.Instance.DecreasePlayerPoints(50);
        }
        else
        {
            Debug.Log(ballNumber + " is not on bingoCard");
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
            PlayerNumbers.Add(Card[row, col]);
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
