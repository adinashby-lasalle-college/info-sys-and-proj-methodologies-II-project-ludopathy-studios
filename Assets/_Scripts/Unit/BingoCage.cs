using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class BingoCage : Singleton<BingoCage>
{
    private TMP_Text displayNumber;
    [SerializeField] private float waitCageRoll = 1f;
    [SerializeField] private float drawInterval = 0.2f;

    private List<Ball> availableBalls = new List<Ball>();
    private List<Ball> calledBalls = new List<Ball>();
    private PlayerInputActions inputActions;
    private bool isRolling = false;

    public static event Action<int> OnBallDrawn;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new PlayerInputActions();
        displayNumber = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.RollCage.performed += ctx => RollCage();
        inputActions.Player.DrawBall.performed += ctx => ToggleRolling();
        GameManager.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        inputActions.Player.RollCage.performed -= ctx => RollCage();
        inputActions.Player.DrawBall.performed -= ctx => ToggleRolling();
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
        }
    }

    private IEnumerator WaitForNewCageRoll()
    {
        yield return new WaitForSeconds(waitCageRoll);
        inputActions.Enable();
    }

    private void DrawBall()
    {
        if (availableBalls.Count == 0)
        {
            displayNumber.text = "All balls called!";
            return;
        }

        Ball drawnBall = availableBalls[UnityEngine.Random.Range(0, availableBalls.Count)];

        availableBalls.Remove(drawnBall);
        calledBalls.Add(drawnBall);

        UpdateDisplay(drawnBall);
        OnBallDrawn?.Invoke(drawnBall.Number);
        GameManager.Instance.UpdateGameState(GameState.Evaluate);
    }

    private void RollCage()
    {
        if (availableBalls.Count == 0)
        {
            displayNumber.text = "All balls called!";
            return;
        }

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

    private void ToggleRolling()
    {
        isRolling = !isRolling;
    }

    public void ReturnBall(int number)
    {
        Ball returnedBall = calledBalls.FirstOrDefault(ball => ball.Number == number);
        if (returnedBall != null)
        {
            calledBalls.Remove(returnedBall);
            availableBalls.Add(returnedBall);
        }
    }

    private void InitializeBalls()
    {
        availableBalls.Clear();
        calledBalls.Clear();
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
            CancelInvoke(nameof(PickRandomBall));
            return;
        }

        Ball randomBall = availableBalls[UnityEngine.Random.Range(0, availableBalls.Count)];
        UpdateDisplay(randomBall);
    }

    private void UpdateDisplay(Ball ball)
    {
        string bingoLetter = GetBingoLetter(ball.Number);
        displayNumber.text = $"{bingoLetter} {ball.Number}";
    }

    private string GetBingoLetter(int number)
    {
        if (number <= 15) return "B";
        if (number <= 30) return "I";
        if (number <= 45) return "N";
        if (number <= 60) return "G";
        return "O";
    }
}
