using UnityEngine;

public class GiantBossParticlesCaller : MonoBehaviour
{
    [SerializeField] private ParticleBanks _particle;
    [SerializeField] private Transform _particlePosition;
    [SerializeField] private ScreenShakeBanks _ScreenShakeProfile;
    public void CallParticles(ParticleBanks particleBank)
    {
        GameManager.Instance.EventBus.Invoke(new SpawnParticlesSignal(particleBank, _particlePosition.position));
        GameManager.Instance.EventBus.Invoke(new ScreenShakeSignal(_ScreenShakeProfile));
    }
}
