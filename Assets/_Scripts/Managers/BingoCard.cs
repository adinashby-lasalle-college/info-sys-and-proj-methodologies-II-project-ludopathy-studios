using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore;

public class BingoCard : Singleton<BingoCard>
{
    private List<Cell> Cells = new List<Cell>();
    protected override void Awake()
    {
        base.Awake();
        GetChildCells();
    }
    void GetChildCells()
    {
        Cells.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Cells.Add(transform.GetChild(i).GetComponent<Cell>());
        }
    }
    private void Start()
    {
        PlayerSetup();
    }
    void OnEnable()
    {
        BingoCage.OnBallDrawn += MarkCell;
    }
    void OnDisable()
    {
        BingoCage.OnBallDrawn -= MarkCell;
    }
    public void MarkCell(int ballNumber)
    {
        //Check for the number in the bingo Card
        if (TryFindCell(ballNumber, out Cell foundCell))
        {
            // Mark the number
            foundCell.MarkNumber();
        }
    }
    void PlayerSetup()
    {
        // Generate a Bingo Card List 
        List<int> Card = BingoCardGenerator.GenerateBingoCard();
        // Initialize cells
        for (int i = 0; i < Cells.Count; i++)
        {
            Cells[i].Init(Card[i]);
        }
    }
    bool TryFindCell(int cellNumber, out Cell cell)
    {
        cell = Cells.Find(p => p.number == cellNumber);
        return cell != null;
    }

    public List<Cell> GetRowCells(Cell cell)
    {
        List<Cell> rowCells = new List<Cell>();

        int cellIndex = Cells.IndexOf(cell);
        if (cellIndex == -1)
        {
            Debug.LogWarning("Cell not found in the list.");
            return rowCells;
        }

        int rowStart = (cellIndex / 5) * 5;

        for (int i = rowStart; i < rowStart + 5; i++)
        {
            if (Cells[i] != cell)
            {
                rowCells.Add(Cells[i]);
            }
        }

        return rowCells;
    }

    public List<Cell> GetColumnCells(Cell cell)
    {
        List<Cell> colCells = new List<Cell>();

        int cellIndex = Cells.IndexOf(cell);
        if (cellIndex == -1)
        {
            Debug.LogWarning("Cell not found in the list.");
            return colCells;
        }
        int colStart = cellIndex % 5;

        for (int i = colStart; i < colStart + 20; i += 5)
        {
            if (Cells[i] != cell)
            {
                colCells.Add(Cells[i]);
            }
        }
        return colCells;
    }
    public void MarkList(List<Cell> cellList)
    {
        foreach (Cell cell in cellList)
        {
            if (cell.isMarked)
            {
                cell.UnmarkNumber();
            }
            else
            {
                cell.MarkNumber();
            }
        }
    }
}
