using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private float _duration = 0.5f;

    public void Init(float duration)
    {
        _duration = duration;
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Color startColor = _text.color;
        startColor.a = 0f;
        _text.color = startColor;

        DoFadeOut();
    }

    public void DoFadeOut() =>_text.DOColor(new Color(_text.color.r, _text.color.g, _text.color.b, 1f), _duration);

    public void DoFadeIn(float time) => _text.DOColor(new Color(_text.color.r, _text.color.g, _text.color.b, 0f), time);


}
