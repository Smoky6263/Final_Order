using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private sbyte _damageValue;

    private const string _playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag && collision.transform.GetComponentInParent<IHealth>() != null)
        {
            collision.gameObject.GetComponentInParent<IHealth>().TakeDamage(_damageValue);
        }
    }
}
