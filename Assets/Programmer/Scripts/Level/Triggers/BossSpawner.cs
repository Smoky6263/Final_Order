using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private EventBusManager _eventBusManager;
    [SerializeField] private PlayableDirector _cutscene;
    [SerializeField] private Transform _spawnPosition;

    public UnityEvent Action;

    private Transform _playerPosition;
    private IControlable _controlable;

    private bool _bossSpawned = false;

    public void SpawnBoss(GameObject bossPrefab)
    {
        if(_bossSpawned) return;
        GameObject boss = Instantiate(bossPrefab, _spawnPosition.position, Quaternion.identity);
        boss.GetComponent<IBoss>().Init(_eventBusManager, _playerPosition);
        _bossSpawned = true;
    }

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
