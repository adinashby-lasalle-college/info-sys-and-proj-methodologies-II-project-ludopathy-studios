using Andres_Scene_Scripts;
using UnityEngine;

public class Twister : BingoCellDecorator
{
    private BingoCard _bingoCard;

    public Twister(IBingoCell cell, BingoCard bingoCard) : base(cell)
    {
        _bingoCard = bingoCard;
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log($"Twister activated on {Number} - affecting the row.");

        // Remove marked numbers in the row and return them to the Bingo Cage
        _bingoCard.RemoveNumbersInRow(Number);
    }
}