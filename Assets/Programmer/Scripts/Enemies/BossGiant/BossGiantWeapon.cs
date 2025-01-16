using UnityEngine;

public class BossGiantWeapon : MonoBehaviour
{
    [SerializeField] private float _damageRadius;
    [SerializeField, Range(0f, 100f)] private float _damageValue;
    [SerializeField, Range(0f, 1f)] private float _throwTime = 0.15f;
    [SerializeField] private Vector2 _damageForce;

    private LayerMask _playerLayer;

    private void Awake() => _playerLayer = GetComponentInParent<BossGiantStateMachine>().PlayerLayer;
    private void Update() => DoAttack();

    private void DoAttack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, _damageRadius, _playerLayer);

        if (hitPlayer != null && hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.OnDamageDelay == false)
        {
            float playerOnRightSide = hitPlayer.transform.position.x > transform.position.x ? 1f : -1f;
            Vector2 applyForce = new Vector2(_damageForce.x * playerOnRightSide, _damageForce.y);
            hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.ApplyDamage(_damageValue, applyForce, _throwTime);
        }
    }

#if UNITY_EDITOR
    [Header("IF IN UNITY EDITOR")]
    [SerializeField] private bool _damageDebugRays;
    [SerializeField] private Color _sphereColor;
    private void OnDrawGizmos()
    {
        Gizmos.color = _sphereColor;
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }

#endif
}
