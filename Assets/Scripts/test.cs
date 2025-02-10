using UnityEngine;


public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int[,] Card = BingoCardGenerator.GenerateBingoCard();


        string output = "";
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                output += Card[i, j] + " ";
            }

            output += "\n";
        }
        Debug.Log(output);
    }



}
