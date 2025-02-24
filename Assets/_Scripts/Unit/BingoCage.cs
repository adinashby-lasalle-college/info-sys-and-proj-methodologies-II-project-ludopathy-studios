using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BingoCage : MonoBehaviour
{
    [SerializeField] int randomNumber;
    [SerializeField] TMP_Text displayNumber;
    [SerializeField] float drawInterval = 0.5f;
    List<int> availableNumbers = new List<int>();
    private bool isDrawing = true;

    public List<int> calledNumbers = new List<int>();

    private void Start()
    {
        InitializeNumbers();
        InvokeRepeating(nameof(CreateRandomNumber), 0.1f, drawInterval);
    }

    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.Space) && isDrawing)
      {
        DrawBall();
        isDrawing = false;
      }
      else if (!isDrawing && Input.GetKeyDown(KeyCode.Space))
      {
        Resume();
        isDrawing = true;
      }
    }

    private void DrawBall()
    {
        CancelInvoke(nameof(CreateRandomNumber));
        Debug.Log(randomNumber);
        availableNumbers.Remove(randomNumber);
        calledNumbers.Add(randomNumber);
    }

    public void Resume()
    {
        InvokeRepeating(nameof(CreateRandomNumber), 0.1f, drawInterval);
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
