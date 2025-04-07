using UnityEngine;

public class JokerPowerUp : BingoCellDecorator
{
    private BingoCellDecorator chosenPowerUp;

    public JokerPowerUp(IBingoCell cell, BingoCellDecorator chosenPowerUp) : base(cell)
    {
        this.chosenPowerUp = chosenPowerUp;
    }

    public override void Activate()
    {
        Debug.Log("Joker activated. Using chosen power-up.");
        chosenPowerUp.Activate();
    }
}