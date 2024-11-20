using UnityEngine.UI;
using UnityEngine;
using System.Numerics;
using Cysharp.Threading.Tasks;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Image _alphaImage;
    [SerializeField] private Image _popUpWindow;
    [SerializeField] private float _alphaImageValue;

    [SerializeField, Range(0f, 2f)] private float _duration;
    [SerializeField] private float _overshoot;

    [SerializeField] private UnityEngine.Vector3 _popUpWindowStartPosition;
    [SerializeField] private UnityEngine.Vector3 _popUpWindowTargetPosition;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        OnEnableAnimation().Forget();
    }

    private async UniTask OnEnableAnimation()
    {
        //_popUpWindow.transform.position = EasingFunctions.EaseOutBack((System.Numerics.Vector3)_popUpWindowStartPosition, _popUpWindowTargetPosition, _duration, _overshoot);
    }
}
