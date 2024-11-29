using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private string busPath;

    private FMOD.Studio.Bus bus;

    private void Start()
    {
        if (!string.IsNullOrEmpty(busPath))
        {
            bus = RuntimeManager.GetBus(busPath);
            bus.getVolume(out float volume);
            slider.value = volume * slider.maxValue;
            UpdateSliderOutput();
        }
    }

    public void UpdateSliderOutput()
    {
        if (text != null && slider != null)
        {
            float percentage = (slider.value / slider.maxValue) * 100f;
            text.text = $"{percentage:0}%";
            bus.setVolume(slider.value / slider.maxValue);
        }
    }
}
