using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] private string _nextLevel;
    [Inject] private GameManager _gameManager;
    [Inject] private IObjectResolver _container;


    public EventBus EventBus { get; private set; }
    public string NextLevel { get { return _nextLevel; } }

    private void Awake()
    {
        EventBus = _gameManager.EventBus;
        EventBus.Subscribe<SpawnUIElementSignal>(SpawnUIElement);
    }

    public void SpawnUIElement(GameObject uiElement)
    {
        GameObject go = _container.Instantiate(uiElement, transform);
        go.transform.SetAsFirstSibling();
    }

    public void SpawnUIElement(SpawnUIElementSignal signal)
    {
        GameObject go = _container.Instantiate(signal.Prefab, transform);
        go.transform.SetAsFirstSibling();
    }
}
