using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public abstract class PopUpPanel : MonoBehaviour
{
    [Header("Настройки продолжительности анимации")]
    [SerializeField, Range(0f, 20f)] private float _duration = 1f;
    [SerializeField, Range(0f, 2f)] private float _overshoot = 2f;

    [Header("Бэкгроунд поля")]
    [SerializeField] private Image _bg;
    [SerializeField, Range(0f,255f)] private float _bgTargetAlpha = 192f;

    [Header("PopUp поля")]
    [SerializeField] private Image _popUpWindow;
    [SerializeField] private Vector3 _popUpWindowStartPosition;
    [SerializeField] private Vector3 _popUpWindowTargetPosition;

    [Header("PopUp поля")]
    [SerializeField] private Button _button;

    protected MainCanvasManager _canvasManager;

    private void Awake()
    {
        _canvasManager = GetComponentInParent<MainCanvasManager>();
        Color startColor = _bg.color;
        startColor.a = 0f;
        _bg.color = startColor;
        _bgTargetAlpha /= 255f;

        _popUpWindow.transform.localPosition = _popUpWindowStartPosition;
        
        if( _button != null )
            _button.interactable = false;
    }

    private void OnEnable()
    {
        _bg.DOFade(_bgTargetAlpha, _duration).SetEase(Ease.OutCubic);
        _popUpWindow.rectTransform.DOLocalMove(_popUpWindowTargetPosition, _duration).SetEase(Ease.OutBack, _overshoot).OnComplete( () => 
            {
                if (_button != null)
                    _button.interactable = true; 
            });

        DoSomethingOnEnable();
    }

    public abstract void DoSomethingOnEnable();
    public abstract void DoSomethingOnDisable();

    public void OnExit()
    {
        _bg.DOFade(0, _duration).SetEase(Ease.InExpo);
        _popUpWindow.rectTransform.DOLocalMove(_popUpWindowStartPosition, _duration).SetEase(Ease.InBack, _overshoot).OnComplete( () =>
        {
            DoSomethingOnDisable();
            Destroy(gameObject);
        });
    }
}