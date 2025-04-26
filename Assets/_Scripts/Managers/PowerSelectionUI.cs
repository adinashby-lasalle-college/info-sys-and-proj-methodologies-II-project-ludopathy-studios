using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public static class PowerSelectionUI
{
    public static PowerUpType SelectedPower { get; private set; } = PowerUpType.None;

    public static void SelectPower(PowerUpType powerType)
    {
        SelectedPower = powerType;
    }

    public static void ClearSelection()
    {
        SelectedPower = PowerUpType.None;
    }
}



