using UnityEngine;

public class MedKitPickUp : MonoBehaviour, IPauseHandler
{
    [SerializeField] private EventBusManager _gameManager;

    private string _playerTag = "Player";
    
    private EventBus _eventBus;
    private PauseManager _pauseManager;
    private Animator _animator;

    private bool _onPause = false;

    public void Init(PauseManager pauseManager)
    {

    }

    private void Awake()
    {
        if (_gameManager == null)
        {
            Debug.LogWarning($"Ты забыл прокинуть ссылки в инспекторе на обьект {this.gameObject.name}!");
            return;
        }

        _eventBus = _gameManager.EventBus;
        
        _pauseManager = _gameManager.GetComponent<PauseManager>();
        _pauseManager.Register(this);
        
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (_onPause) return;

        if (collision.gameObject.tag == _playerTag)
        {
            collision.transform.GetComponentInParent<PlayerStateMachine>().PayerHealth.OnMedKitPickUp();
            _eventBus.Invoke(new PickUpMedKitSignal());
            Destroy(this.gameObject);
        }
    }
    public void SetPause()
    {
        _animator.speed = 0f;
        _onPause = true;
    }

    public void SetPlay()
    {
        _animator.speed = 1f;
        _onPause = false;
    }

    public void Unregister()
    {
        _pauseManager.Unregister(this);

    }

    public void OnDestroy() => Unregister();
}
