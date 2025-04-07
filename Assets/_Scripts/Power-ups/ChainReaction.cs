using Andres_Scene_Scripts;
using UnityEngine;

public class ChainReaction : BingoCellDecorator
{
    private BingoCard _bingoCard;

    public ChainReaction(IBingoCell cell, BingoCard bingoCard) : base(cell)
    {
        _bingoCard = bingoCard;
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("Chain Reaction activated. Triggering all power-ups in the column.");
        _bingoCard.TriggerColumnPowerUps(Number);
    }
}