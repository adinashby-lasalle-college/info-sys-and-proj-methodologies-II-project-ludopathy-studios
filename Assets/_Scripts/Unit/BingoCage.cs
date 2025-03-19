using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;



public class BingoCage : Singleton<BingoCage>
{
    [SerializeField] int randomNumber;
    [SerializeField] TMP_Text displayNumber;
    [SerializeField] float drawInterval = 0.2f;
    List<int> availableNumbers = new List<int>();

    public List<int> calledNumbers = new List<int>();
    public static event Action<int> OnBallDrawn;

    private PlayerInputActions inputActions;

    private bool isRolling = true;
    protected override void Awake()
    {
        base.Awake();
        inputActions = new PlayerInputActions();
    }
    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.RollCage.performed += ctx => RollCage();
        inputActions.Player.DrawBall.performed += ctx => ToggleAction();
        GameManager.OnStateChanged += OnStateChanged;

    }

    void OnDisable()
    {
        inputActions.Player.RollCage.performed += ctx => RollCage();
        inputActions.Player.DrawBall.performed += ctx => ToggleAction();
        inputActions.Disable();
        GameManager.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.GameInit:
                InitializeNumbers();
                break;
            case GameState.BallDrawing:
                StartCoroutine(WaitForNewCageRoll());
                break;
            case GameState.Evaluate:
                inputActions.Disable();
                break;
            default:
                Debug.Log("State not implemented");
                break;
        }
    }

    IEnumerator WaitForNewCageRoll()
    {
        yield return new WaitForSeconds(2f);
        inputActions.Enable();
        Debug.Log("You can roll the cage now");
    }
    private void DrawBall()
    {
        CancelInvoke(nameof(CreateRandomNumber));
        availableNumbers.Remove(randomNumber);
        calledNumbers.Add(randomNumber);
        OnBallDrawn?.Invoke(randomNumber);
        GameManager.Instance.UpdateGameState(GameState.Evaluate);
    }
    private void RollCage()
    {
        if (!isRolling)
        {
            InvokeRepeating(nameof(CreateRandomNumber), 0, drawInterval);
        }
        else
        {
            DrawBall();
        }
    }

    private void ToggleAction()
    {
        isRolling = !isRolling;
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
