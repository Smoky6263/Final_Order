using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField, Range(0f, 100f)] private float _damageValue;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(transform.position + _offset, _boxSize, 0f, _playerLayer);

        if (hitPlayer != null && hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PayerHealth.OnDamageDelay == false)
        {
            hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PayerHealth.GetDamage(_damageValue);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + _offset, _boxSize);
    }
#endif
}
