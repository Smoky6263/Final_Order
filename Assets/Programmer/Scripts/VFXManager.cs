using DG.Tweening.Core.Easing;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private VFXPrefabs _vfxPrefabs;
    
    private PauseManager _pauseManager;
    private Vector3 _offset = new Vector3(0f, 0f, 1f);

    private void Awake() => _pauseManager = GetComponent<PauseManager>();

    #region Particles
    public void SpawnDustParticles(Vector3 position)
    {
        GameObject particles = Instantiate(_vfxPrefabs.p_DustParticlesPrefab, position + _offset, Quaternion.identity);
        particles.GetComponent<VFXScript>().Init(_pauseManager);
    }
    public void SpawnBloodParticles(Vector3 position)
    {
        GameObject particles = Instantiate(_vfxPrefabs.p_BloodtParticlesPrefab, position + _offset, Quaternion.identity);
        particles.GetComponent<VFXScript>().Init(_pauseManager);
    }
    #endregion

    #region Materials
    public Material GetDamageMaterial()
    {
        return _vfxPrefabs.m_EnemyGetDamage;
    }
    #endregion
}
