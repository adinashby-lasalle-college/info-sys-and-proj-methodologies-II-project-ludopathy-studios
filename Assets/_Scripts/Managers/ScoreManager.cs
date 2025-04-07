using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] TMP_Text playerPointsUI;
    public int playerPoints;
    public float currentMultiplier;

    void OnEnable()
    {
        Cell.OnCellMarked += OnCellMarked;
    }

    void OnDisable()
    {
        Cell.OnCellMarked -= OnCellMarked;
    }

    private void OnCellMarked(Cell cell, bool isMarked, int scorePoints)
    {
        if (isMarked)
        {
            IncreasePlayerPoints(scorePoints);
        }
        else
        {
            DecreasePlayerPoints(scorePoints);
        }
    }
    void Start()
    {
        playerPoints = 0;
        currentMultiplier = 1;
        playerPointsUI.text = "Player Score: 0";
    }

    private void IncreasePlayerPoints(int points)
    {
        float addPoints = points * currentMultiplier;
        playerPoints += (int)addPoints;
        playerPointsUI.text = "Player Score: " + playerPoints;
    }
    private void DecreasePlayerPoints(int points)
    {
        playerPoints -= points;
        playerPointsUI.text = "Player Score: " + playerPoints;
    }

}
