using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public abstract void ApplyPower(Cell cell);
}

public class DoublePointsPower : PowerUp
{
    public override void ApplyPower(Cell cell)
    {
        cell.pointsToScore *= 2;
    }
}

public class Tornado : PowerUp
{
    public override void ApplyPower(Cell cell)
    {
        List<Cell> rowCells = BingoCard.Instance.GetRowCells(cell);

        BingoCard.Instance.MarkList(rowCells);
    }
}

