using UnityEngine;

public class MedKitPickUp : MonoBehaviour
{
    [SerializeField] private EventBusManager _gameManager;

    private string _playerTag = "Player";
    private EventBus _eventBus;

    private void Awake()
    {
        if (_gameManager == null)
            Debug.LogWarning($"�� ����� ��������� ������ � ���������� �� ������ {this.gameObject.name}!");

        _eventBus = _gameManager.EventBus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            collision.transform.GetComponentInParent<PlayerStateMachine>().PayerHealth.OnMedKitPickUp();
            _eventBus.Invoke(new PickUpMedKitSignal());
            Destroy(this.gameObject);
        }
    }
}
