using UnityEngine;

public class EnemyWithWeaponDamageTrigger : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _damageValue;
    [SerializeField, Range(0f, 1f)] private float _throwTime = 0.15f;

    [SerializeField] private Vector2 _damageForce;

    [SerializeField] private Vector2 _DamageBoxSize;
    [SerializeField] private Vector3 _offset;

    private float _facingRight;
    private float _facingLeft;

    private EnemyWithWeaponStateMachine _enemyData;
    private LayerMask _playerLayer;

    private void Awake() 
    {
        _enemyData = GetComponentInParent<EnemyWithWeaponStateMachine>();
        _playerLayer = _enemyData.PlayerLayer;

        _facingRight = _offset.x * -1f;
        _facingLeft = _offset.x;
    }

    public void DoAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(transform.position + _offset, _DamageBoxSize, 0f, _playerLayer);

        if (hitPlayer != null && hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.OnDamageDelay == false)
        {
            float playerOnRightSide = hitPlayer.transform.position.x > transform.position.x ? 1f : -1f;
            Vector2 applyForce = new Vector2(_damageForce.x * playerOnRightSide, _damageForce.y);
            hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.ApplyDamage(_damageValue, applyForce, _throwTime);
        }
    }

    public void AttackComplete()
    {
        _enemyData.OnAttack = false;
    }

    public void WeaponFacingRight(bool right)
    {
        if (right) _offset = new Vector3(_facingRight, _offset.y, 0f);

        else _offset = new Vector3(_facingLeft, _offset.y, 0f);
    }

#if UNITY_EDITOR
    #region Debug Vars
    [Header("IF IN UNITY EDITOR")]
    [SerializeField] private bool _debugDrawBox = true;

    private void OnDrawGizmos()
    {
        if(_debugDrawBox)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + _offset, _DamageBoxSize);
        }
    }

    #endregion
#endif
}
