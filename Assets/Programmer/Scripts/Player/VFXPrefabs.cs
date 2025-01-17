using UnityEngine;

[CreateAssetMenu(menuName = "VFX prefabs")]
public class VFXPrefabs : ScriptableObject
{
    #region Particle System

    [Header("PARTICLES SYSTEM")]
    [Header("Dust Particles")]
    public GameObject p_DustParticlesPrefab;
    [Range(0f,1f)] public float DustVerticalOffset = 0.2f;
    
    [Header("Blood Particles")]
    public GameObject p_BloodtParticlesPrefab;

    [Header("Player Healing Particles")]
    public GameObject p_HealingParticlesPrefab;

    #endregion

    #region Materials

    [Header("MATERIALS")]
    public Material EnemyDamageMAT;
    public Material PlayerDamageMAT;

    #endregion
}
