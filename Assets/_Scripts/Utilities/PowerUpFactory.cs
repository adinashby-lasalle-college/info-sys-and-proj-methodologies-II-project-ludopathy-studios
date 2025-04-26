using UnityEngine;

// Factory design pattern 
public class PowerUpFactory
{
    public static PowerUp AddPowerToCell(PowerUpType type, GameObject cellObject)
    {
        switch (type)
        {
            case PowerUpType.DoublePoints:
                return cellObject.AddComponent<DoublePointsPower>();
            case PowerUpType.Tornado:
                return cellObject.AddComponent<Tornado>();
            case PowerUpType.Bomberman:
                return cellObject.AddComponent<Bomberman>();
            default:
                Debug.LogWarning("Invalid or None PowerUp selected");
                return null;
        }
    }
}

public enum PowerUpType
{
    None,
    DoublePoints,

    Tornado,

    Bomberman
}
