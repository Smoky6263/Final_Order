using AYellowpaper.SerializedCollections;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyFactory : MonoBehaviour
{
    [SerializedDictionary("Enemy Type", "Enemy Prefab"), SerializeField]
    private SerializedDictionary<int, GameObject> _enemies;

    private IObjectResolver _container;
    
    private Arena _arena;

    private int _waveCount;

    [Inject]
    private void Construct(IObjectResolver container)
    {
        _container = container;
    }

    private void Start() 
    {
        _arena = GetComponentInParent<Arena>();
    }

    public void SpawnNewWave()
    {
        GameObject enemy = _container.Instantiate(_enemies[_waveCount], transform.position, Quaternion.identity);
        ArenaListController arena = enemy.transform.GetChild(2).gameObject.AddComponent<ArenaListController>();
        arena.Init(_arena);

        enemy.transform.GetChild(0).transform.position = transform.GetChild(0).position;
        enemy.transform.GetChild(1).transform.position = transform.GetChild(1).position;

        _arena._enemyList.Add(arena);

        _waveCount++;
    }
}
