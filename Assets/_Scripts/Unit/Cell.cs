using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Cell : MonoBehaviour
{
    private CellPowerUpManager powerUpManager;
    private TMP_Text btnText;
    private Button btn;
    public int number { get; private set; }
    public bool isMarked { get; private set; }

    void Awake()
    {
        btn = GetComponent<Button>();
        btnText = GetComponentInChildren<TMP_Text>();
        powerUpManager = GetComponent<CellPowerUpManager>();
    }
    public void Init(int number)
    {
        this.number = number;
        btnText.text = number.ToString();
        isMarked = false;

    }
    public void MarkNumber()
    {
        powerUpManager.TriggerPowers(this);
        btn.interactable = false;
        isMarked = true;
    }
    public void UnmarkNumber()
    {
        btn.interactable = true;
        isMarked = false;
    }
}

