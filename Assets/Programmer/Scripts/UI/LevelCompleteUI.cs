using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class LevelCompleteUI : MonoBehaviour
{
    [Header("Настройки продолжительности анимации")]
    [SerializeField, Range(0f, 20f)] private float _duration;
    [SerializeField, Range(0f, 2f)] private float _overshoot;

    [Header("Бэкгроунд поля")]
    [SerializeField] private Image _bg;
    [SerializeField] private float _bgTargetAlpha;

    [Header("PopUp поля")]
    [SerializeField] private Image _popUpWindow;
    [SerializeField] private Vector3 _popUpWindowStartPosition;
    [SerializeField] private Vector3 _popUpWindowTargetPosition;

    [Header("PopUp поля")]
    [SerializeField] private Button _nextLevelButton;

    private void Awake()
    {
        Color startColor = _bg.color;
        startColor.a = 0f;
        _bg.color = startColor;
        _bgTargetAlpha /= 255f;

        _popUpWindow.transform.localPosition = _popUpWindowStartPosition;
        _nextLevelButton.interactable = false;
    }

    private void OnEnable()
    {
        _bg.DOFade(_bgTargetAlpha, _duration).SetEase(Ease.OutCubic);
        _popUpWindow.rectTransform.DOLocalMove(_popUpWindowTargetPosition, _duration).SetEase(Ease.OutBack, _overshoot).OnComplete( () => _nextLevelButton.interactable = true);
    }

    public void OnExit()
    {
        _bg.DOFade(0, _duration).SetEase(Ease.InExpo);
        _popUpWindow.rectTransform.DOLocalMove(_popUpWindowStartPosition, _duration).SetEase(Ease.InBack, _overshoot).OnComplete( () => Destroy(gameObject));
    }
}