using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Component design pattern 
public class CellPowerUpManager : MonoBehaviour
{
    public List<PowerUp> activePowers = new List<PowerUp>();

    public void AddPower(PowerUp newPower)
    {
        // Avoids power duplication on the same cell
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
}
