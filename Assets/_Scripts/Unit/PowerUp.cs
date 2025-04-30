using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public abstract class PowerUp : MonoBehaviour
{
    public abstract IEnumerator ApplyPowerCoroutine(Cell cell);
    public void ApplyPower()
    {
        StartCoroutine(ApplyPowerCoroutine(GetComponent<Cell>()));
    }
}

public class DoublePointsPower : PowerUp
{
    public override IEnumerator ApplyPowerCoroutine(Cell cell)
    {
        cell.pointsToScore *= 2;

        yield return null;

        Destroy(this);
    }
}

public class Tornado : PowerUp
{
    public override IEnumerator ApplyPowerCoroutine(Cell cell)
    {
        List<Cell> rowCells = BingoCard.Instance.GetRowCells(cell);

        BingoCard.Instance.MarkList(rowCells);

        yield return new WaitForSeconds(1f);

        Destroy(this);

    }
}

public class Bomberman : PowerUp
{
    public override IEnumerator ApplyPowerCoroutine(Cell cell)
    {
        List<Cell> rowCells = BingoCard.Instance.GetRowCells(cell);

        BingoCard.Instance.MarkList(rowCells);

        List<Cell> colCells = BingoCard.Instance.GetColumnCells(cell);

        BingoCard.Instance.MarkList(colCells);

        yield return new WaitForSeconds(1f);

        Destroy(this);

    }
}

