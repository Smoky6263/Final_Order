public class SliderValueSetSignal
{
    public readonly SoundType SliderType;
    public readonly float Value;

    public SliderValueSetSignal (SoundType sliderType, float value)
    {
        SliderType = sliderType;
        Value = value;
    }
}