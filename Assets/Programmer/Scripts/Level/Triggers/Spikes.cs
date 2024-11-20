using UnityEngine;
using Cinemachine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private sbyte _damageValue;
    [SerializeField] ScreenShakeProfile _shakeProfile;

    private const string _playerTag = "Player";

    private CinemachineImpulseSource _source;

    private void Start()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            collision.gameObject.GetComponentInParent<PlayerStateMachine>().PayerHealth.GetDamage(_damageValue);

            //ScreenShake
        }
    }
}
