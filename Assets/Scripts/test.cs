using UnityEngine;


public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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



}
