using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField, Range(0f, 100f)] private float _damageValue;

    [SerializeField] private Vector2 _damageForce;

    [SerializeField] private Vector2 _DamageBoxSize;
    [SerializeField] private Vector3 _offset;
    

    private void FixedUpdate()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(transform.position + _offset, _DamageBoxSize, 0f, _playerLayer);

        if (hitPlayer != null && hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.OnDamageDelay == false)
        {
            float playerOnRightSide = hitPlayer.transform.position.x > transform.position.x ? 1f : -1f;
            Vector2 applyForce = new Vector2(_damageForce.x * playerOnRightSide, _damageForce.y);
            hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.GetDamage(_damageValue, applyForce);
        }
    }

#if UNITY_EDITOR
    #region Debug Vars
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + _offset, _DamageBoxSize);
    }

    #endregion
#endif
}
