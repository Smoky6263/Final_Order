using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField, Range(0f, 100f)] private float _maxHealth; 
    [SerializeField, Range(0f, 100f)] private float _health;
    
    private EventBus _eventBus;
    private int _medKitsCount;

    private void Awake()
    {
        if (_gameManager == null)
            Debug.LogWarning($"Ты забыл прокинуть ссылки в инспекторе на обьект {gameObject.name}!");

        _eventBus = _gameManager.EventBus;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    public void TakeDamage(float value)
    {
        _health -= value;

        if(_health < 0 )
            _health = 0;

        _eventBus.Invoke(new PlayerHealthChangeSignal(_health));
    }
    public void ImproveHealth()
    {
        if(_medKitsCount > 0 && _health < _maxHealth)
        {
            _health = _maxHealth;
            _eventBus.Invoke(new PlayerHealthChangeSignal(_health));
            _eventBus.Invoke(new MedKitPerformedSignal());
            _medKitsCount--;
        }
    }

    public void OnMedKitPickUp() => _medKitsCount++;
}
