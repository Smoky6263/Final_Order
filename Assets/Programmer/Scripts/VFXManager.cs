using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private VFXPrefabs _vfxPrefabs;

    [SerializeField] private Color _enemyBlood;
    [SerializeField] private Color _playerBlood;

    public Color EnemyBlood { get {  return _enemyBlood; } }
    public Color PlayerBlood { get { return _playerBlood; } }

    private PauseManager _pauseManager;
    private Vector3 _offset = new Vector3(0f, 0f, 1f);

    private void Awake() => _pauseManager = GetComponent<PauseManager>();

    #region Particles
    public void SpawnDustParticles(Vector3 position)
    {
        GameObject particles = Instantiate(_vfxPrefabs.p_DustParticlesPrefab, position + _offset, Quaternion.identity);
        particles.GetComponent<VFXScript>().Init(_pauseManager);
    }
    public void SpawnBloodParticles(Vector3 position, Color bloodColor)
    {
        GameObject particles = Instantiate(_vfxPrefabs.p_BloodtParticlesPrefab, position + _offset, Quaternion.identity);
        particles.GetComponent<VFXScript>().Init(_pauseManager);
        ParticleSystem.MainModule mainModule = particles.GetComponent<ParticleSystem>().main;
        mainModule.startColor = bloodColor;
    }
    #endregion

    #region Materials
    public Material GetDamageMaterial()
    {
        return _vfxPrefabs.m_EnemyGetDamage;
    }
    #endregion
}
