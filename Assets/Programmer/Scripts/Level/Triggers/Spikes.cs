using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private sbyte _damageValue;
    [SerializeField] private Vector2 _damageForce;

    private const string _playerTag = "Player";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            float playerOnRightSide = collision.transform.position.x > transform.position.x ? 1f : -1f;
            Vector2 applyForce = new Vector2(_damageForce.x * playerOnRightSide, _damageForce.y);
            collision.gameObject.GetComponentInParent<PlayerStateMachine>().PlayerHealth.GetDamage(_damageValue, applyForce);
        }
    }
}
