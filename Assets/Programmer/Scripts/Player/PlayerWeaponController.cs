using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private LayerMask _enemyLayer;
    
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private Vector3 _boxOffset;

    private float _damageValue;

    public float Box_X_value {  get; private set; }
    public Vector3 BoxOffset { get { return _boxOffset; } set { _boxOffset = value; } }

    private void Awake()
    {
        Box_X_value = _boxOffset.x;
        _damageValue = _playerStats._weaponDamage;
    }

    public void DoAttack()
    {
        Collider2D hitEnemy = Physics2D.OverlapBox(transform.position + _boxOffset, _boxSize, 0f, _enemyLayer);

        if (hitEnemy != null)
        {
            hitEnemy.GetComponent<IEnemy>().HealthManager.GetDamage(_damageValue);
        }
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
