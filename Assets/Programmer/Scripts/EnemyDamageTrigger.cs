using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField, Range(0f, 100f)] private float _damageValue;
    
    private const string _playerTag = "Player";


    private void FixedUpdate()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(transform.position + _offset, _boxSize, 0f, _playerLayer);

        if (hitPlayer != null)
            hitPlayer.gameObject.GetComponentInParent<PlayerStateMachine>().PayerHealth.TakeDamage(_damageValue);
    }

#if UNITY_EDITOR
    #region Debug Vars
    [Header("������������ �������� ��� ��������� ����� �������")]
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private Vector3 _offset;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + _offset, _boxSize);
    }

    #endregion
#endif
}