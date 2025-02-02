using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    private EventBus _eventBus;
    private Animator _animator;
    private Slider _hpBar;

    private int _fadeOutAnimationHash;

    private void Awake()
    {
        _hpBar = GetComponent<Slider>();
        _fadeOutAnimationHash = Animator.StringToHash("BossGiantHPFadeOut");
    }

    private void Start()
    {
        _eventBus = GameManager.Instance.EventBus;
        _eventBus.Subscribe<BossOnHealthChangeSignal>(BossOnHealthChange);
        _eventBus.Subscribe<TurnOffBossHealthBarSignal>(TurnOffBar);
    }

    private void BossOnHealthChange(BossOnHealthChangeSignal signal)
    {
        _hpBar.value = signal.Value;
    }

    private void TurnOffBar(TurnOffBossHealthBarSignal signal)
    {
        GetComponent<Animator>().Play(_fadeOutAnimationHash);
    }

    public void DestroyHealthBar() => Destroy(this);
}
