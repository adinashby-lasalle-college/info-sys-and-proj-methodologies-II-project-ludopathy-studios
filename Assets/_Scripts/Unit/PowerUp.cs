using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public abstract void ApplyPower(Cell cell);
}

public class DoublePointsPower : PowerUp
{
    public override void ApplyPower(Cell cell)
    {
        cell.scorePoints *= 2;
    }
}

