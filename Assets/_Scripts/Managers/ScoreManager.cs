using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] TMP_Text playerPointsUI;
    public int playerPoints;
    public float currentMultiplier;

    void Start()
    {
        playerPoints = 0;
        currentMultiplier = 1;
        playerPointsUI.text = "Player Score: 0";
    }

    public void IncreasePlayerPoints(int points)
    {
        float addPoints = points * currentMultiplier;
        playerPoints += (int)addPoints;
        playerPointsUI.text = "Player Score: " + playerPoints;
    }

    public void DecreasePlayerPoints(int points)
    {
        playerPoints -= points;
        playerPointsUI.text = "Player Score: " + playerPoints;
    }


}
