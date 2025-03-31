using System.Collections.Generic;
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
}
