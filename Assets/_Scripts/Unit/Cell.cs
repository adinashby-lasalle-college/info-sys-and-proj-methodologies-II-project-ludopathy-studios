using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Cell : MonoBehaviour
{
    private CellPowerUpManager powerUpManager;
    public static event Action<Cell, bool, int> OnCellMarked;
    private TMP_Text btnText;
    private Button btn;
    public int number { get; private set; }
    public int scorePoints;
    public bool isMarked { get; private set; }


    void Awake()
    {
        btn = GetComponent<Button>();
        btnText = GetComponentInChildren<TMP_Text>();
        powerUpManager = GetComponent<CellPowerUpManager>();

        btn.onClick.AddListener(OnCellClicked);
    }

    private void OnCellClicked()
    {
        if (PowerSelectionUI.SelectedPowerType != null)
        {

        }
    }
    public void Init(int number)
    {
        this.number = number;
        btnText.text = number.ToString();
        isMarked = false;
        scorePoints = 50;
    }
    public void MarkNumber()
    {
        powerUpManager?.TriggerPowers(this);
        btn.interactable = false;
        isMarked = true;
        OnCellMarked?.Invoke(this, isMarked, scorePoints);
    }
    public void UnmarkNumber()
    {
        btn.interactable = true;
        isMarked = false;
        OnCellMarked?.Invoke(this, isMarked, scorePoints);
    }
}

