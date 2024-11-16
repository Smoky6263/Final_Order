using UnityEngine;

public class VFXScript : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    [SerializeField] private float _lifeTime = 2f;

    private void Awake() => _particleSystem = GetComponent<ParticleSystem>();
    private void Start() => _particleSystem.Play();

    private void FixedUpdate()
    {
        _lifeTime -= Time.fixedDeltaTime;

        if(_lifeTime < 0 )
            Destroy(gameObject);
    }
}
