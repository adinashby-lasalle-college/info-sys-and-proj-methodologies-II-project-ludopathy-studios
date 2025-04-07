using UnityEngine;

public class Ludopathy : BingoCellDecorator
{
    private float multiplier = 2.0f;
    private float penalty = 0.75f;
    private int chosenNumber;
    
    public Ludopathy(IBingoCell cell, int chosenNumber) : base(cell)
    {
        this.chosenNumber = chosenNumber;
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log($"Ludopathy activated. Current multiplier: {multiplier}x");

        if (Number == chosenNumber)
        {
            Debug.Log($"Penalty! Multiplier reduced to {penalty}x");
            multiplier = penalty;
        }
        else
        {
            multiplier += 0.25f;
        }
    }
}