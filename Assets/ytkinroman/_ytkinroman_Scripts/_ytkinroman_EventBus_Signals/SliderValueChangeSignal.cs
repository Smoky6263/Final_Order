public class SliderValueChangeSignal
{
    public readonly SoundType SliderType;
    public readonly float Value;


    public SliderValueChangeSignal (SoundType sliderType, float value)
    {
        SliderType = sliderType;
        Value = value;
    }
}
