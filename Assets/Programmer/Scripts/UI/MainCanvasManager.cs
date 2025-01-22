using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] private int _nextLevel;
    

    public EventBus EventBus { get; private set; }
    public int NextLevel { get { return _nextLevel; } }

    private void Awake()
    {
        EventBus = GameManager.Instance.EventBus;
        EventBus.Subscribe<SpawnBossHPSignal>(SpawnUIElement);
    }

    public void SpawnUIElement(GameObject uiElement)
    {
        Instantiate(uiElement, transform);
    }

    public void SpawnUIElement(SpawnBossHPSignal signal)
    {
        Instantiate(signal.HPPrefab, transform);
    }
}
