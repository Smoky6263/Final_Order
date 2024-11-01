using Unity.VisualScripting;
using UnityEngine;

public class MedKitPickUp : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private string _playerTag = "Player";
    private EventBus _eventBus;

    private void Awake()
    {
        if (_gameManager == null)
            Debug.LogWarning($"Ты забыл прокинуть ссылки в инспекторе на обьект {this.gameObject.name}!");

        _eventBus = _gameManager.EventBus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag && collision.transform.GetComponentInParent<IHealth>() != null)
        {
            collision.transform.GetComponentInParent<IHealth>().OnMedKitPickUp();
            _eventBus.Invoke(new PickUpMedKitSignal());
            Destroy(this.gameObject);
        }
    }
}
