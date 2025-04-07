using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.InputSystem;


public class BingoCage : Singleton<BingoCage>
{
    [SerializeField] int randomNumber;
    [SerializeField] TMP_Text displayNumber;
    [SerializeField] float drawInterval = 0.2f;
    List<int> availableNumbers = new List<int>();

    public List<int> calledNumbers = new List<int>();
    public static event Action<int> OnBallDrawn;

    private InputAction drawBallAction;
    private InputAction rollCageAction;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnStateChanged += OnStateChanged;
    }

    void Start()
    {
        drawBallAction = InputSystem.actions.FindAction("DrawBall");
        drawBallAction.Disable();
        rollCageAction = InputSystem.actions.FindAction("RollCage");
        rollCageAction.Disable();
    }

    private void OnDestroy()
    {
        GameManager.OnStateChanged -= OnStateChanged;
    }
    void Update()
    {
        if (rollCageAction.IsPressed())
        {
            RollCage();
            drawBallAction.Enable();
            return;
        }
        else if (drawBallAction.IsPressed())
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
                StartCoroutine(waitForNewCageRoll());
                break;
            default:
                Debug.Log("State not implemented");
                break;
        }
    }

    IEnumerator waitForNewCageRoll()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("You can roll the cage now");
        rollCageAction.Enable();
    }
    private void DrawBall()
    {
        drawBallAction.Disable();
        CancelInvoke(nameof(CreateRandomNumber));
        availableNumbers.Remove(randomNumber);
        calledNumbers.Add(randomNumber);
        OnBallDrawn?.Invoke(randomNumber);
        GameManager.Instance.UpdateGameState(GameState.Evaluate);
    }
    public void RollCage()
    {
        rollCageAction.Disable();
        InvokeRepeating(nameof(CreateRandomNumber), 0, drawInterval);
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

    public void ChooseNextNumber(int chosenNumber)
    {
        if (!availableNumbers.Contains(chosenNumber))
    {
        Debug.Log($"Number {chosenNumber} is either already drawn or invalid.");
        return;
    }

    availableNumbers.Remove(chosenNumber);
    calledNumbers.Add(chosenNumber);

    displayNumber.text = GetBingoLetter(chosenNumber) + " " + chosenNumber;
    Debug.Log($"Sacrifice Power-Up: {chosenNumber} has been chosen as the next number.");

    OnBallDrawn?.Invoke(chosenNumber);

    GameManager.Instance.UpdateGameState(GameState.Evaluate);
    }

    public void ReturnNumberToCage(int numberToReturn)
    {
            if (!availableNumbers.Contains(numberToReturn))
    {
        availableNumbers.Add(numberToReturn);
        calledNumbers.Remove(numberToReturn);
        Debug.Log($"ReturnNumberToCage: {numberToReturn} added back to available numbers.");
    }
    else
    {
        Debug.LogWarning($"ReturnNumberToCage: {numberToReturn} is already in the available pool.");
    }
    }

    public void ChooseNextNumber()
    {
            // Disable other input actions temporarily
    drawBallAction.Disable();
    rollCageAction.Disable();

    // You can prompt the player through the UI to select a number
    Debug.Log("ChooseNextNumber: Player must choose a number to draw.");

    // Example for development/testing: You simulate a chosen number
    // Replace this with your actual UI logic to let the player pick
    int chosenNumber = SimulatePlayerChoosingNumber();

    if (availableNumbers.Contains(chosenNumber))
    {
        availableNumbers.Remove(chosenNumber);
        calledNumbers.Add(chosenNumber);
        displayNumber.text = GetBingoLetter(chosenNumber) + " " + chosenNumber;
        OnBallDrawn?.Invoke(chosenNumber);

        GameManager.Instance.UpdateGameState(GameState.Evaluate);
    }
    else
    {
        Debug.LogWarning($"ChooseNextNumber: Number {chosenNumber} is not available.");
    }
}

// TEMP: This is just for testing — replace with real UI later
private int SimulatePlayerChoosingNumber()
{
    // Just return a random number from the available ones for now
    return availableNumbers[UnityEngine.Random.Range(0, availableNumbers.Count)];
    }
}
