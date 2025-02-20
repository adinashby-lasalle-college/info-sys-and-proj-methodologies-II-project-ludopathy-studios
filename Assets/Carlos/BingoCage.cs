using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BingoCage : MonoBehaviour
{
    [SerializeField] int randomNumber;
    [SerializeField] TMP_Text displayNumber;
    [SerializeField] float drawInterval = 5f;
    List<int> availableNumbers = new List<int>();

    private void Start()
    {
        InitializeNumbers();
        InvokeRepeating(nameof(CreateRandomNumber), 1f, drawInterval);
    }

    private void InitializeNumbers()
    {
        availableNumbers.Clear();
        for (int i = 1; i <= 75; i++)
        {
            availableNumbers.Add(i);
        }
    }

    private void CreateRandomNumber()
    {
                if (availableNumbers.Count == 0)
        {
            displayNumber.text = "All numbers drawn!";
            CancelInvoke(nameof(CreateRandomNumber)); // Stop drawing when finished
            return;
        }

        int randomIndex = Random.Range(0, availableNumbers.Count);
        randomNumber = availableNumbers[randomIndex];
        availableNumbers.RemoveAt(randomIndex); // Remove drawn number from the list

        // Get the corresponding Bingo letter
        string bingoLetter = GetBingoLetter(randomNumber);

        // Update the UI with the letter and number
        displayNumber.text = bingoLetter + " " + randomNumber;
        Debug.Log("Drawn Number: " + bingoLetter + " " + randomNumber);
    }

        private string GetBingoLetter(int number)
    {
        if (number >= 1 && number <= 15) return "B";
        if (number >= 16 && number <= 30) return "I";
        if (number >= 31 && number <= 45) return "N";
        if (number >= 46 && number <= 60) return "G";
        if (number >= 61 && number <= 75) return "O";
        return "";
    }
}
