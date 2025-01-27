using UnityEngine;

public class SpawnParticlesSignal
{
    public readonly ParticleBanks ParticleBanks;
    public readonly Vector3 Position;
    public SpawnParticlesSignal(ParticleBanks particleBanks, Vector3 position)
    {
        ParticleBanks = particleBanks;
        Position = position;
    }
}
