using UnityEngine;

public class Sacrifice : BingoCellDecorator
{
    private BingoCage _bingoCage;

    public Sacrifice(IBingoCell cell, BingoCage bingoCage) : base(cell)
    {
        _bingoCage = bingoCage;
    }

    public override void Activate()
    {
        Debug.Log("Sacrifice activated. Choose any number to be drawn.");
        _bingoCage.ChooseNextNumber();
    }
}