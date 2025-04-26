using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public abstract class PowerUp : MonoBehaviour
{
    public abstract IEnumerator ApplyPower(Cell cell);
}

public class DoublePointsPower : PowerUp
{
    public override IEnumerator ApplyPower(Cell cell)
    {
        cell.pointsToScore *= 2;

        yield return null;
    }
}

public class Tornado : PowerUp
{
    public override IEnumerator ApplyPower(Cell cell)
    {
        List<Cell> rowCells = BingoCard.Instance.GetRowCells(cell);

        BingoCard.Instance.MarkList(rowCells);

        yield return null;
    }
}

public class Bomberman : PowerUp
{
    public override IEnumerator ApplyPower(Cell cell)
    {
        List<Cell> rowCells = BingoCard.Instance.GetRowCells(cell);

        BingoCard.Instance.MarkList(rowCells);

        List<Cell> colCells = BingoCard.Instance.GetColumnCells(cell);

        BingoCard.Instance.MarkList(colCells);

        yield return null;

    }
}

