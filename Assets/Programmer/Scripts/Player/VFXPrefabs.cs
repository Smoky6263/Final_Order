using UnityEngine;

[CreateAssetMenu(menuName = "VFX prefabs")]
public class VFXPrefabs : ScriptableObject
{
    [Header("Dust Particles")]
    public GameObject DustParticlesPrefab;
    [Range(0f,1f)] public float DustVerticalOffset = 0.2f;
}
