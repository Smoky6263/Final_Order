using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private Slider comboTimerSlider;
    [SerializeField] private TMP_Text feedbackText;

    private string[] feedbackLevels = { "Норм...", "Харош!", "Мегахарош!!!", "ШИЗ", "МЕГАШИЗ!!!!!!" };

    private void OnEnable()
    {
        if (ComboSystem.Instance != null)
        {
            ComboSystem.Instance.OnComboUpdated += UpdateUI;
            ComboSystem.Instance.OnComboEnded += ResetUI;
        }
    }

    private void OnDisable()
    {
        if (ComboSystem.Instance != null)
        {
            ComboSystem.Instance.OnComboUpdated -= UpdateUI;
            ComboSystem.Instance.OnComboEnded -= ResetUI;
        }
    }

    private void Update()
    {
        // Плавное уменьшение значения слайдера
        if (ComboSystem.Instance != null && comboTimerSlider != null)
        {
            float targetValue = ComboSystem.Instance.ComboWindowProgress;
            comboTimerSlider.value = Mathf.Lerp(comboTimerSlider.value, targetValue, Time.deltaTime * 10f);
        }
    }

    private void UpdateUI(int comboCount, int comboMultiplier)
    {
        // Combo counter
        comboText.text = $"Combo: x{comboCount}";

        // Combo feedback
        int feedbackIndex = Mathf.Clamp(comboCount - 1, 0, feedbackLevels.Length - 1);
        feedbackText.text = feedbackLevels[feedbackIndex];

        // Combo txt color
        feedbackText.color = Color.Lerp(feedbackText.color, Color.green, Time.deltaTime * 5f);

        // Slider goes UP
        comboTimerSlider.value = Mathf.Lerp(comboTimerSlider.value, 1f, Time.deltaTime * 10f);
    }

    private void ResetUI(int score)
    {
        comboText.text = "Combo Ended";
        feedbackText.text = " ";
        comboTimerSlider.value = 0f;
    }
}
