using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private sbyte _damageValue;
    [SerializeField, Range(0f, 1f)] private float _throwTime = 0.15f;
    [SerializeField] private Vector2 _damageForce;
    
    private const string _enemyTag = "Enemy";
    private const string _playerTag = "Player";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            float playerOnRightSide = collision.transform.position.x > transform.position.x ? 1f : -1f;
            Vector2 applyForce = new Vector2(_damageForce.x * playerOnRightSide, _damageForce.y);
            collision.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.ApplyDamage(_damageValue, applyForce, _throwTime);
        }
        else if (collision.gameObject.tag == _enemyTag)
        {
            collision.GetComponentInParent<IEnemy>().HealthManager.ApplyDamage(999999f, Vector2.zero);
        }
    }
}
