using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private Slider comboTimerSlider;
    [SerializeField] private TMP_Text feedbackText;

    private void OnEnable()
    {
        if (ComboSystem.Instance != null)
        {
            ComboSystem.Instance.OnComboUpdated += UpdateUI;
            ComboSystem.Instance.OnComboEnded += ResetUI;
        }
        else
        {
            Debug.LogError("ComboSystem.Instance is null. Ensure ComboSystem exists in the scene.");
        }
    }


    private void OnDisable()
    {
        ComboSystem.Instance.OnComboUpdated -= UpdateUI;
        ComboSystem.Instance.OnComboEnded -= ResetUI;
    }

    private void UpdateUI(int comboCount, int multiplier)
    {
        comboText.text = $"Combo: x{comboCount}";

        if (multiplier <= 3)
            feedbackText.text = "Нормас";
        else if (multiplier <= 6)
            feedbackText.text = "Харош!";
        else
            feedbackText.text = "МегаХарош!!!";

        comboTimerSlider.value = ComboSystem.Instance.ComboWindowProgress;
    }

    private void ResetUI(int finalScore)
    {
        comboText.text = "Combo: x0";
        comboTimerSlider.value = 0;
        feedbackText.text = string.Empty;
    }
}
