using UnityEngine;

public class PowerUpFactory
{
    public static PowerUp AddPowerToCell(PowerUpType type, GameObject cellObject)
    {
        switch (type)
        {
            case PowerUpType.DoublePoints:
                return cellObject.AddComponent<DoublePointsPower>();
            default:
                Debug.LogWarning("Invalid or None PowerUp selected");
                return null;
        }
    }
}
