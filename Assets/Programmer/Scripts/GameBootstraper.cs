using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameBootstraper : LifetimeScope
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private MainCanvasManager _mainCanvasManager;
    [SerializeField] private PauseManager _pauseManager;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameManager).AsSelf();
        if (_mainCanvasManager != null && _pauseManager != null) 
        {
            builder.RegisterInstance(_pauseManager).AsSelf();
            builder.RegisterInstance(_mainCanvasManager).AsSelf();
        }
        builder.RegisterBuildCallback(OnContainerCreated);
    }

    private void OnContainerCreated(IObjectResolver container)
    {
        foreach (var injectableGameObject in FindObjectsOfType<InjectableGameObject>())
        {
            container.InjectGameObject(injectableGameObject.gameObject);
        }
    }
}