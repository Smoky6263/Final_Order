using UnityEngine;

public class GiantBossParticlesCaller : MonoBehaviour
{
    [SerializeField] private ParticleBanks _particle;
    [SerializeField] private Transform _particlePosition;
    [SerializeField] private ScreenShakeBanks _ScreenShakeProfile;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponentInParent<BossGiantStateMachine>().GameManager;
    }

    public void CallParticles(ParticleBanks particleBank)
    {
        _gameManager.EventBus.Invoke(new SpawnParticlesSignal(particleBank, _particlePosition.position));
        _gameManager.EventBus.Invoke(new ScreenShakeSignal(_ScreenShakeProfile));
    }
}
