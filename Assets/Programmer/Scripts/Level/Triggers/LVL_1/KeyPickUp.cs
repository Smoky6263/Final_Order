using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class KeyPickUp : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    private const string _playerTag = "Player";

    private SoundsController _soundsController;

    private void Awake()
    {
        _soundsController = GetComponent<SoundsController>();
        _soundsController.SoundsManager = _gameManager.GetComponent<SoundsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _playerTag)
        {
            collision.transform.AddComponent<Key>();
            _gameManager.EventBus.Invoke(new OnKeyPickedUp());
            _soundsController.MedkKitPickUp();
            Destroy(gameObject);
        }
    }
}
