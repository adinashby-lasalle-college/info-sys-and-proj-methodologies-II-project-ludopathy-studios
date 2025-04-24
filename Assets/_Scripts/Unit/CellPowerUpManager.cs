using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Component design pattern 
public class CellPowerUpManager : MonoBehaviour
{
    public List<PowerUp> activePowers = new List<PowerUp>();

    public void AddPower(PowerUp newPower)
    {
        if (activePowers.Any(item => item.GetType() == newPower.GetType()))
        {
            Destroy(newPower);
            Debug.Log("Power already on cell");
        }
        else
        {
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
