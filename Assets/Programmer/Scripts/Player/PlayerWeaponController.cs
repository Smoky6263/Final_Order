using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private LayerMask _enemyLayer, _breakableWallLayer;
    
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private Vector3 _boxOffset;

    private EventBus _eventBus;
    private float _damageValue;

    public float Box_X_value {  get; private set; }
    public Vector3 BoxOffset { get { return _boxOffset; } set { _boxOffset = value; } }

    public void Init(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        Box_X_value = _boxOffset.x;
        _damageValue = _playerStats._weaponDamage;
    }

    public void DoAttack()
    {
        Collider2D hitEnemy = Physics2D.OverlapBox(transform.position + _boxOffset, _boxSize, 0f, _enemyLayer);
        Collider2D hitWall = Physics2D.OverlapBox(transform.position + _boxOffset, _boxSize, 0f, _breakableWallLayer);

        if (hitEnemy != null)
            hitEnemy.GetComponent<IEnemy>().HealthManager.GetDamage(_damageValue);

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
