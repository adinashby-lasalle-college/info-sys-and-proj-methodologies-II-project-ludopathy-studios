using Andres_Scene_Scripts;
using UnityEngine;

public class Bomberman : BingoCellDecorator
{
    private BingoCard _bingoCard;

    public Bomberman(IBingoCell cell, BingoCard bingoCard) : base(cell)
    {
        _bingoCard = bingoCard;
    }

    public override void Activate()
    {
        Debug.Log("Bomberman activated. Drawing row and column numbers.");
        _bingoCard.DrawRowAndColumnNumbers(Number);
    }
}