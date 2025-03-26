using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;



public class BingoCage : Singleton<BingoCage>
{
    [SerializeField] TMP_Text displayNumber;
    [SerializeField] float waitCageRoll = 1f;
    [SerializeField] float drawInterval = 0.2f;
    public List<Ball> availableBalls = new List<Ball>();
    public List<Ball> calledBalls = new List<Ball>();

    public static event Action<int> OnBallDrawn;
    private PlayerInputActions inputActions;
    Ball randomBall;
    bool isRolling = true;
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
                InitializeBalls();
                break;
            case GameState.BallDrawing:
                StartCoroutine(WaitForNewCageRoll());
                break;
            case GameState.Evaluate:
                inputActions.Disable();
                break;
            default:
                break;
        }
    }

    IEnumerator WaitForNewCageRoll()
    {
        yield return new WaitForSeconds(waitCageRoll);
        inputActions.Enable();
    }
    private void DrawBall()
    {
        availableBalls.Add(randomBall);
        calledBalls.Remove(randomBall);

        OnBallDrawn?.Invoke(randomBall.Number);
        GameManager.Instance.UpdateGameState(GameState.Evaluate);
    }
    private void RollCage()
    {
        if (!isRolling)
        {
            InvokeRepeating(nameof(PickRandomBall), 0, drawInterval);
        }
        else
        {
            CancelInvoke(nameof(PickRandomBall));
            DrawBall();
        }
    }

    private void ToggleAction()
    {
        isRolling = !isRolling;
    }

    public void ReturnBall(int number)
    {
        foreach (Ball ball in calledBalls)
        {
            if (ball.Number == number)
            {
                availableBalls.Add(ball);
                calledBalls.Remove(ball);
                break;
            }
        }
    }
    private void InitializeBalls()
    {
        availableBalls.Clear();
        for (int i = 1; i <= 75; i++)
        {
            availableBalls.Add(new Ball(i));
        }
    }

    private void PickRandomBall()
    {
        if (availableBalls.Count == 0)
        {
            displayNumber.text = "All balls called!";
            CancelInvoke(nameof(PickRandomBall)); // Stop drawing when finished
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, availableBalls.Count);
        randomBall = availableBalls[randomIndex];


        // Get the corresponding Bingo letter
        string bingoLetter = GetBingoLetter(randomBall.Number);

        // Update the UI with the letter and number
        displayNumber.text = bingoLetter + " " + randomBall.Number;

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
