using Cinemachine;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private LayerMask _enemyLayer, _breakableWallLayer;

    [SerializeField] private Vector2 _damageForce;

    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private Vector3 _boxOffset;

    [SerializeField] private ScreenShakeProfile _profile;
    
    private CinemachineImpulseSource _impulseSource;

    private EventBus _eventBus;
    private float _damageValue;

    public float DamageBox_X_value {  get; private set; }
    public Vector3 BoxOffset { get { return _boxOffset; } set { _boxOffset = value; } }


    private void Awake()
    {
        DamageBox_X_value = _boxOffset.x;
        _damageValue = _playerStats._weaponDamage;
    }

    private void Start()
    {
        _eventBus = GetComponentInParent<PlayerStateMachine>().EventBus;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void DoAttack()
    {
        Collider2D hitEnemy = Physics2D.OverlapBox(transform.position + _boxOffset, _boxSize, 0f, _enemyLayer);
        Collider2D hitWall = Physics2D.OverlapBox(transform.position + _boxOffset, _boxSize, 0f, _breakableWallLayer);

        if (hitEnemy != null)
        {
            float forceDirection = hitEnemy.transform.position.x < transform.position.x ? -1f : 1f;
            Vector2 applyDamageForce = new Vector2(_damageForce.x * forceDirection, _damageForce.y);
            hitEnemy.GetComponentInParent<IEnemy>().HealthManager.ApplyDamage(_damageValue, applyDamageForce);
            CameraShakeManager.instance.ScreenShakeFromProfile(_profile, _impulseSource);
        }

        if (hitWall != null)
            hitWall.GetComponent<BreakebleWallController>().GetDamage(_damageValue);

        _eventBus.Invoke(new PlayerAttackAnimationCompleteSignal());

}

#if UNITY_EDITOR

[SerializeField] private Color _color;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireCube(transform.position + _boxOffset, _boxSize);
    }

#endif

}
