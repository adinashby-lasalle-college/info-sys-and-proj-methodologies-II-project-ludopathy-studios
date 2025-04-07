using UnityEngine;

public class PowerSelectionUI : MonoBehaviour
{
    public static System.Type SelectedPowerType { get; private set; }

    public void SelectDoublePoints()
    {
        SelectedPowerType = typeof(DoublePointsPower);
    }
}
