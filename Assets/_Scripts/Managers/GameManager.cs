using System;
using System.Collections;
using Andres_Scene_Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    [SerializeField] TMP_Text turnText;
    public static event Action<GameState> OnStateChanged;

    private int currentTurn;
    private int maxTurns = 15;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        UpdateGameState(GameState.GameInit);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        OnStateChanged?.Invoke(newState);

        switch (newState)
        {
            case GameState.Win:
                Debug.Log("You win");
                break;
            case GameState.Lose:
                Debug.Log("You Lose");
                break;
            case GameState.GameInit:
                GameInit();
                break;
            case GameState.PerkSelection:
                Debug.Log("PerkSelection");
                break;
            case GameState.PowerUpSelection:
                Debug.Log("PowerUpSelection");
                break;
            case GameState.PowerUpPlacement:
                Debug.Log("PowerUpPlacement");
                break;
            case GameState.BallDrawing:
                Debug.Log("BallDrawing state");
                break;
            case GameState.Evaluate:
                Debug.Log("Evaluating");
                EvaluateState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void GameInit()
    {
        currentTurn = 0;
        turnText.text = "Turn: " + currentTurn + "/" + maxTurns;
        UpdateGameState(GameState.BallDrawing);
    }
    private void EvaluateState()
    {
        currentTurn++;
        turnText.text = "Turn: " + currentTurn + "/" + maxTurns;
        if (BingoCard.Instance.isBingo())
        {
            UpdateGameState(GameState.Win);
        }
        else if (currentTurn >= maxTurns)
        {
            UpdateGameState(GameState.Lose);
        }
        else
        {
            UpdateGameState(GameState.BallDrawing);
        }
    }

}
public enum GameState
{
    GameInit,
    PowerUpSelection,
    PowerUpPlacement,
    PerkSelection,
    BallDrawing,
    Evaluate,
    PredictChoosing,
    Lose,
    Win,

}
