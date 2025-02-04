using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuBootStrap : LifetimeScope
{
    [SerializeField] private GameManager _gameManager;

    protected override void Configure (IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameManager).AsSelf();


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
