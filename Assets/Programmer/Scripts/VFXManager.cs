using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private VFXPrefabs _vfxPrefabs;

    private Vector3 _offset = new Vector3(0f, 0f, 1f);

    public void SpawnDustParticles() => Instantiate(_vfxPrefabs.DustParticlesPrefab, transform.position + _offset, Quaternion.identity);
}
