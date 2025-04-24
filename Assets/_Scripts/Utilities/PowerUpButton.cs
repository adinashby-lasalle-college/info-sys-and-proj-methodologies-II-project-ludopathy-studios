using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    public PowerUpType powerType;
    private TMP_Text btnText;
    private Button button;

    void Awake()
    {
        btnText = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
    }
    void Start()
    {
        btnText.text = powerType.ToString();
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        PowerSelectionUI.SelectPower(powerType);

        Debug.Log(PowerSelectionUI.SelectedPower.ToString());
    }
}
