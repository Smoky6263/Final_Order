using FMODUnity;
using UnityEngine;
[RequireComponent(typeof(ShakerScript))]
public class BreakebleWallController : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    
    [SerializeField] private Transform _spriteTransform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _spriteMaterial;

    [Header("Статы для тряски стены")]
    [SerializeField, Range(0f, 2f)] private float _duration = 0.5f;
    [SerializeField, Range(0f, 1f)] float _intensity = 0.5f;
    
    private ShakerScript _shaker;

    private void Awake()
    {
        _shaker = GetComponent<ShakerScript>();

    }

    public void GetDamage(float value)
    {
        RuntimeManager.PlayOneShot("event:/SFX/BreakebleWall Hit");
        _health -= value;

        StartCoroutine(_shaker.ShakeSpriteRendererCoroutine(_spriteTransform, _spriteRenderer, _spriteMaterial, _duration, _intensity));

        if (_health < 0)
            Destroy(gameObject);
    }

    
}
