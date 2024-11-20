using System;
using UnityEngine;

public class MedKitsUI : MonoBehaviour
{
    [SerializeField] private EventBusManager _gameManager;
    [SerializeField] private GameObject _healPrefab;

    private EventBus _eventBus;

    private void Awake()
    {
        if (_gameManager == null)
            Debug.LogWarning($"Ты забыл прокинуть ссылки в инспекторе на обьект {gameObject.name}!");

        _eventBus = _gameManager.EventBus;
        _eventBus.Subscribe<PickUpMedKitSignal>(OnMedKitPickUp);
        _eventBus.Subscribe<MedKitPerformedSignal>(OnMedkitPerformed);
    }

    private void OnMedKitPickUp(PickUpMedKitSignal signal)
    {
        Instantiate(_healPrefab, transform);
    }

    private void OnMedkitPerformed(MedKitPerformedSignal signal)
    {
        if(transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }
}
