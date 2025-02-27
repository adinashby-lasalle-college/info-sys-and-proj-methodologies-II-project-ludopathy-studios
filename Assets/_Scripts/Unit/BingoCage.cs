using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;


public class BingoCage : Singleton<BingoCage>
{
    [SerializeField] int randomNumber;
    [SerializeField] TMP_Text displayNumber;
    [SerializeField] float drawInterval = 0.5f;
    List<int> availableNumbers = new List<int>();
    private bool isDrawing = false;
    public List<int> calledNumbers = new List<int>();

    public static event Action<int> OnBallDrawn;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnStateChanged -= OnStateChanged;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDrawing)
        {
            DrawBall();
        }
    }
    private void OnStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.GameInit:
                InitializeNumbers();
                break;
            case GameState.BallDrawing:
                isDrawing = true;
                break;
            default:
                Debug.Log("State not implemented");
                break;
        }
    }

    private void DrawBall()
    {
        isDrawing = false;
        CancelInvoke(nameof(CreateRandomNumber));
        availableNumbers.Remove(randomNumber);
        calledNumbers.Add(randomNumber);
        OnBallDrawn?.Invoke(randomNumber);
        GameManager.Instance.UpdateGameState(GameState.Evaluate);
    }


    public void Resume()
    {
        isDrawing = true;
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

        int randomIndex = UnityEngine.Random.Range(0, availableNumbers.Count);
        randomNumber = availableNumbers[randomIndex];


        // Get the corresponding Bingo letter
        string bingoLetter = GetBingoLetter(randomNumber);

        // Update the UI with the letter and number
        displayNumber.text = bingoLetter + " " + randomNumber;
        //Debug.Log("Drawn Number: " + bingoLetter + " " + randomNumber);
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
