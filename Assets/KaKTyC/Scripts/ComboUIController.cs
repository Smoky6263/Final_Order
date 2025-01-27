using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboUIController : MonoBehaviour
{
    [SerializeField] private Image comboText;
    [SerializeField] private Image feedbackText;
    [SerializeField] private Slider comboTimerSlider;

    [SerializeField] private ComboSystem _comboSystem;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private Sprite[] comboTextLevels;
    [SerializeField] private Sprite[] feedbackLevels;

    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeIntensity;

    private void Start()
    {
        ComboSystem.Instance.OnComboUpdated += UpdateUI;
        ComboSystem.Instance.OnComboEnded += ResetUI;
        ChangeAlpha(0f);

    }

    private void OnDisable()
    {
        ComboSystem.Instance.OnComboUpdated -= UpdateUI;
        ComboSystem.Instance.OnComboEnded -= ResetUI;       
    }

    private void Update()
    {
        // Плавное уменьшение значения слайдера
        
        float targetValue = ComboSystem.Instance.ComboWindowProgress;
        comboTimerSlider.value = Mathf.Lerp(comboTimerSlider.value, targetValue, Time.deltaTime * 10f);
    }

    private void UpdateUI(int comboCount, int comboMultiplier)
    {
        ChangeAlpha(1f);
        // Combo counter
        comboText.sprite = comboCount <= comboTextLevels.Length - 1 ? comboTextLevels[comboCount - 1] : comboTextLevels[comboTextLevels.Length - 1];

        // Combo feedback
        int feedbackIndex = Mathf.Clamp(comboCount - 1, 0, feedbackLevels.Length - 1);

        StartCoroutine(feedbackText.GetComponent<ShakerScript>().ShakeImageCoroutine(feedbackText.rectTransform, _shakeDuration, _shakeIntensity));
        feedbackText.sprite = comboCount <= comboTextLevels.Length - 1 ? feedbackLevels[comboCount - 1] : feedbackLevels[comboTextLevels.Length - 1];

        // Combo txt color
        //feedbackText.color = Color.Lerp(feedbackText.color, Color.green, Time.deltaTime * 5f);

        // Slider goes UP
        comboTimerSlider.value = Mathf.Lerp(comboTimerSlider.value, 1f, Time.deltaTime * 10f);
    }

    private void ResetUI(int score)
    {
        ChangeAlpha(0f);
        comboTimerSlider.value = 0f;
    }
    /// <summary>
    /// 1 = выключить прозрачность, 0 = включить прозрачность.
    /// </summary>
    /// <param name="value"></param>
    private void ChangeAlpha(float value)
    {
        _canvasGroup.alpha = value;
    }
}
