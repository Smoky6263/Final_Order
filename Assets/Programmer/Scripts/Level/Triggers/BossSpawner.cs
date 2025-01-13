using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private EventBusManager _eventBusManager;
    [SerializeField] private Transform _spawnPosition;

    private Transform _playerPosition;
    
    public UnityEvent Action;


    public void SpawnBoss(GameObject bossPrefab)
    {
        GameObject boss = Instantiate(bossPrefab, _spawnPosition.position, Quaternion.identity);
        boss.GetComponent<IBoss>().Init(_eventBusManager, _playerPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _playerPosition = collision.gameObject.transform;
            Action?.Invoke();
        }
    }
}
