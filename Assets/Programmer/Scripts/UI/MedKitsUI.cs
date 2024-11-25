using System;
using UnityEngine;

public class MedKitsUI : MonoBehaviour
{
    [SerializeField] private GameObject _healPrefab;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = GetComponentInParent<MainCanvasManager>().EventBus;

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
