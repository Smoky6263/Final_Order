using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class BossSpawner : MonoBehaviour
{
    [SerializeField] private PlayableDirector _cutscene;
    [SerializeField] private Transform _spawnPosition;

    public UnityEvent Action;

    private GameManager _gameManager;
    private Transform _playerPosition;
    private IControlable _controlable;
    private CharacterController _characterController;

    private IBoss _boss;
    private bool _bossSpawned = false;

    private void Start() => _gameManager = GameManager.Instance;

    public void SpawnBoss(GameObject bossPrefab)
    {
        if(_bossSpawned) return;
        GameObject boss = Instantiate(bossPrefab, _spawnPosition.position, Quaternion.identity);
        _boss = boss.GetComponent<IBoss>();
        _boss.Init(_gameManager, _playerPosition);
        _bossSpawned = true;
    }
    public void PlayerOnCutScene() => _playerPosition.GetComponentInParent<PlayerStateMachine>().OnCutScene = true;
    public void BossOnRoar() => _gameManager.EventBus.Invoke(new ScreenShakeSignal(ScreenShakeBanks.GiantBossRoar));
    public void SpawnBossHP(GameObject HPPrefab) => _gameManager.EventBus.Invoke(new SpawnBossHPSignal(HPPrefab));
    public void FightBegin()
    {
        _playerPosition.GetComponentInParent<PlayerStateMachine>().OnCutScene = false;
        _boss.OnCutScene = false;
        _boss.OnPause = false;
        _characterController.enabled = true;

        _characterController = null;
        _controlable = null;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _controlable = collision.GetComponentInParent<IControlable>();
            _characterController = collision.GetComponentInParent<CharacterController>();
            _characterController.enabled = false;
            _playerPosition = collision.gameObject.transform;
            Action?.Invoke();
            PlayerOnCutScene();
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
        }
    }
}
