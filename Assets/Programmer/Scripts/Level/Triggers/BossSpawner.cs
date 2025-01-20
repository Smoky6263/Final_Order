using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameManager _eventBusManager;
    [SerializeField] private PlayableDirector _cutscene;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private ScreenShakeProfile _screenShakeProfile;

    public UnityEvent Action;

    private Transform _playerPosition;
    private IControlable _controlable;
    private CinemachineImpulseSource _impulseSource;

    private IBoss _boss;
    private bool _bossSpawned = false;

    private void Awake() => _impulseSource = GetComponent<CinemachineImpulseSource>();

    public void SpawnBoss(GameObject bossPrefab)
    {
        if(_bossSpawned) return;
        GameObject boss = Instantiate(bossPrefab, _spawnPosition.position, Quaternion.identity);
        _boss = boss.GetComponent<IBoss>();
        _boss.Init(_eventBusManager, _playerPosition);
        _bossSpawned = true;
    }

    public void BossOnRoar() => CameraShakeManager.instance.ScreenShakeFromProfile(_screenShakeProfile, _impulseSource);
    public void SpawnBossHP(GameObject HPPrefab) => _eventBusManager.EventBus.Invoke(new SpawnBossHPSignal(HPPrefab));
    public void TurnOffBossPause() => _boss.OnPause = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _controlable = collision.GetComponentInParent<IControlable>();
            _playerPosition = collision.gameObject.transform;
            Action?.Invoke();
            _cutscene.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_controlable != null) _controlable.MoveInput(1f, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_controlable != null)
        {
            _controlable.MoveInput(0f, 0f);
            _controlable = null;
        }
    }
}
