using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Image _bg;
    [SerializeField] private float _bgTargetAlpha;

    [SerializeField] private Image _popUpWindow;
    [SerializeField] private Vector3 _popUpWindowStartPosition;
    [SerializeField] private Vector3 _popUpWindowTargetPosition;

    [SerializeField, Range(0f, 20f)] private float _duration;

    private void Awake()
    {
        Color startColor = _bg.color;
        startColor.a = 0f;
        _bg.color = startColor;
        _bgTargetAlpha /= 255f;

        _popUpWindow.transform.localPosition = _popUpWindowStartPosition;
    }

    private void OnEnable()
    {
        _bg.DOFade(_bgTargetAlpha, _duration).SetEase(Ease.OutCubic);
        _popUpWindow.rectTransform.DOLocalMove(_popUpWindowTargetPosition, _duration).SetEase(Ease.OutCubic);
    }
}