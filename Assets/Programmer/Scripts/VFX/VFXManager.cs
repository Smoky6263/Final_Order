using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum ParticleBanks
    {
        p_Dust, 
        p_EnemyBlood,
        p_PlayerBlood,
        p_Healing
    }
public class VFXManager : MonoBehaviour
{
    #region Particle System
    [SerializedDictionary("Particle Names", "Particle GameObjects"), SerializeField]
    private SerializedDictionary<ParticleBanks, GameObject> _particleBanks;

    #endregion

    #region Materials

    [Header("MATERIALS")]
    public Material EnemyDamageMAT;
    public Material PlayerDamageMAT;

    #endregion

    private EventBus _eventBus;

    private PauseManager _pauseManager;
    private Vector3 _offset = new Vector3(0f, 0f, 1f);

    private void Start() 
    {
        _eventBus = GameManager.Instance.EventBus;
        _eventBus.Subscribe<SpawnParticlesSignal>(SpawnParticles);
        _pauseManager = GetComponent<PauseManager>();
    }

    #region Particles
    public void SpawnParticles(SpawnParticlesSignal signal)
    {
        GameObject particles = Instantiate(_particleBanks[signal.ParticleBanks], signal.Position + _offset, Quaternion.identity);
        particles.GetComponent<VFXScript>().Init(_pauseManager);
    }
    #endregion

    #region Materials
    public Material EnemyDamageMaterial()
    {
        return EnemyDamageMAT;
    }

    public Material PlayerDamageMaterial()
    {
        return PlayerDamageMAT;
    }
    #endregion

}
