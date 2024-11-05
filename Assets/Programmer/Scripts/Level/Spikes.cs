using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private sbyte _damageValue;

    private const string _playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            collision.gameObject.GetComponentInParent<PlayerStateMachine>().PayerHealth.TakeDamage(_damageValue);
        }
    }
}
