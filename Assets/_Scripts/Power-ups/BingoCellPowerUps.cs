using UnityEngine;

public interface IBingoCell
{
    int Number { get; }
    void Activate();
}

public class BingoCellPowerUps : IBingoCell
{
    public int Number { get; private set; }

    public BingoCellPowerUps(int number)
    {
        Number = number;
    }

    public virtual void Activate()
    {
        Debug.Log($"Number {Number} has been marked.");
    }
}

public abstract class BingoCellDecorator : IBingoCell
{
    protected IBingoCell _cell;

    public int Number => _cell.Number;

    protected BingoCellDecorator(IBingoCell cell)
    {
        _cell = cell;
    }

    public virtual void Activate()
    {
        _cell.Activate();
    }
}