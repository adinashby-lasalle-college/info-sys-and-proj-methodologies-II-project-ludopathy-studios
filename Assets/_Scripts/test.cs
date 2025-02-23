using System.Linq.Expressions;
using UnityEngine;


public class test : MonoBehaviour
{

    void Awake()
    {
        GameManager.OnGameInit += DisplayBingoCard;
    }

    void OnDestroy()
    {
        GameManager.OnGameInit -= DisplayBingoCard;
    }
    private void DisplayBingoCard(GameState newState)
    {
        if (newState == GameState.GameInit)
        {
            int[,] Card = BingoCardGenerator.GenerateBingoCard();


            string output = "";
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    output += Card[row, col] + " ";
                }

                output += "\n";
            }
            Debug.Log(output);
        }

        GameManager.Instance.UpdateGameState(GameState.BallDrawing);
    }
}
