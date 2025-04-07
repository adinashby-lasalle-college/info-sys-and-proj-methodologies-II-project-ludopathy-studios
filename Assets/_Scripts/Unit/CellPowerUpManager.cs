using System.Collections.Generic;
using UnityEngine;

public class CellPowerUpManager : MonoBehaviour
{
    private List<PowerUp> activePowers = new List<PowerUp>();

    public void AddPower<T>() where T : PowerUp
    {
        if (GetComponent<T>() == null)
        {
            PowerUp newPower = gameObject.AddComponent<T>();
            activePowers.Add(newPower);
        }
    }

    public void TriggerPowers(Cell cell)
    {
        foreach (var power in new List<PowerUp>(activePowers))
        {
            power.ApplyPower(cell);
            activePowers.Remove(power);
            Destroy(power);
        }
    }
}
