using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;

    public static event Action<GameState> OnGameInit;

    void Start()
    {
        UpdateGameState(GameState.GameInit);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            case GameState.GameInit:
                OnGameInit?.Invoke(newState);
                break;
            case GameState.PerkSelection:
                break;
            case GameState.PowerUpSelection:
                break;
            case GameState.PowerUpPlacement:
                break;
            case GameState.BallDrawing:
                Debug.Log("Ball being drawn");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
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
    PredictChoosing,
    Lose,
    Win,

}
