using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WebSliderController : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;

    [SerializeField]
    private TextMeshProUGUI text = null;

    [SerializeField]
    private string busPath = "";


    private FMOD.Studio.Bus bus;

    private void Start()
    {
        if (busPath != "")
        {
            bus = RuntimeManager.GetBus(busPath);
        }

        bus.getVolume(out float volume);
        slider.value = volume ;

        slider.value = 0.8f;
        UpdateSliderOutput();
    }

    public void UpdateSliderOutput()
    {
        if (slider != null)
        {
            int percentage = Mathf.RoundToInt(slider.value * 100);
            text.text = percentage + "%";

            bus.setVolume(slider.value / slider.maxValue);
        }
    }
}
