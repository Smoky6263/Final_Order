using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class Arena : MonoBehaviour
{
    [SerializeField] private int _maxWaves = 1;
    [SerializeField] private List<EnemyFactory> _enemyFactories;
    [SerializeField] private KeyPickUp _keyPrefab;

    [Inject] private GameManager _gameManager;
    [Inject] private IObjectResolver _container;
    public List<ArenaListController> _enemyList { get; set; } = new();

    private int _vaweCount = 0;

    private void Awake()
    {
        _gameManager.EventBus.Subscribe<OnArenaFightBeginSignal>(OnSpawnNewWaveEvent);
    }

    private void OnSpawnNewWaveEvent(OnArenaFightBeginSignal signal)
    {
        SpawnNewWave();
    }

    public void SpawnNewWave()
    {
        foreach (var enemyFactory in _enemyFactories) 
        {
            enemyFactory.SpawnNewWave();
        }
        
        _vaweCount++;
        _gameManager.EventBus.Invoke(new OnVaweCountUpdtaeSignal(_vaweCount));
        _gameManager.EventBus.Invoke(new OnEnemyCountUpdateSignal(_enemyList.Count));
    }

    public void OnEnemyDeath(ArenaListController enemy)
    {
        _enemyList.Remove(enemy);
        _gameManager.EventBus.Invoke(new OnEnemyCountUpdateSignal(_enemyList.Count));


        if (_enemyList.Count == 0 && _vaweCount <= _maxWaves) 
        {
            SpawnNewWave();
        }
        else if(_enemyList.Count == 0 && _vaweCount > _maxWaves)
        {
            GameObject key = _container.Instantiate(_keyPrefab.gameObject, enemy.transform.position + new Vector3(0f,0.5f,0f), Quaternion.identity);
            _container.InjectGameObject(key);
            _gameManager.EventBus.Invoke(new OnArenaPassedSignal());
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Fight", 0);

        }
    }
}
