using System.Collections.Generic;
using Andres_Scene_Scripts;
using UnityEngine;

public interface IPowerUp
{
    void Power();
}

public class Tornado : IPowerUp
{
    int index;

    public Tornado(int _index)
    {
        index = _index;
    }
    public void Power()
    {
        //Get all the rows where the power up is placed 
        List<int> rowIndexes = BingoSelectionHandler.Instance.GetRowIndexes(index);
        //Toggle the buttons 
        foreach (int index in rowIndexes)
        {
            if (BingoCard.Instance.MarkedSpace[index] == 0)
            {
                BingoCard.Instance.MarkNumber(BingoCard.Instance.Numbers[index]);
                BingoCage.Instance.availableNumbers.Add(BingoCard.Instance.Numbers[index]);
            }
            else
            {
                BingoCard.Instance.ReturnBall(BingoCard.Instance.Numbers[index]);
                BingoCage.Instance.availableNumbers.Remove(BingoCard.Instance.Numbers[index]);
            }
        }



    }


}