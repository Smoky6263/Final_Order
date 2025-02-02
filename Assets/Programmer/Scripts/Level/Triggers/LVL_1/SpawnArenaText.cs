using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SpawnArenaText : MonoBehaviour
{
    [SerializeField] private MainCanvasManager _mainCanvasManager;
    [SerializeField] private GameObject _startArenaFightPrefab;

    [Inject] IObjectResolver _container;
    [Inject] GameManager _gameManager;
    [Inject] private PauseManager _pauseManager;

    public void SpawnText()
    {
        _container.Instantiate(_startArenaFightPrefab, _mainCanvasManager.transform.position, Quaternion.identity, _mainCanvasManager.transform);
    }

}
