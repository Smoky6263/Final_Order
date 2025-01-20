using UnityEngine;

public class ParticlesCaller : MonoBehaviour
{
    public void CallParticles(ParticleBanks particleBank)
    {
        GameManager.Instance.EventBus.Invoke(new SpawnParticlesSignal(particleBank, transform.position));
    }
}
