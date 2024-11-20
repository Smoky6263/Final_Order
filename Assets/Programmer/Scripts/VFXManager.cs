using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private VFXPrefabs _vfxPrefabs;

    private Vector3 _offset = new Vector3(0f, 0f, 1f);


    #region Particles
    public void SpawnDustParticles(Vector3 position) => Instantiate(_vfxPrefabs.p_DustParticlesPrefab, position + _offset, Quaternion.identity);
    public void SpawnBloodParticles(Vector3 position) => Instantiate(_vfxPrefabs.p_BloodtParticlesPrefab, position + _offset, Quaternion.identity);
    #endregion

    #region Materials
    public Material GetDamageMaterial()
    {
        return _vfxPrefabs.m_EnemyGetDamage;
    }
    #endregion
}
