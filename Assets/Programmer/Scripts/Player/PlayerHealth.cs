
public class PlayerHealth : IHealth
{
    public PlayerHealth(PlayerStateMachine stateMachine)
    {
        _playerStats = stateMachine;
        _eventBus = _playerStats.EventBus;
        _maxHealth = _playerStats._maxHealth;
        _playerStats._health = _maxHealth;
    }

    private EventBus _eventBus;
    private PlayerStateMachine _playerStats;
    
    private float _maxHealth;
    private int _medKitsCount;


    public void TakeDamage(float value)
    {
        _playerStats._health -= value;

        if(_playerStats._health < 0 )
            _playerStats._health = 0;

        _eventBus.Invoke(new PlayerHealthChangeSignal(_playerStats._health));
    }
    public void ImproveHealth()
    {
        if(_medKitsCount > 0 && _playerStats._health < _maxHealth)
        {
            _playerStats._health = _maxHealth;
            _eventBus.Invoke(new PlayerHealthChangeSignal(_playerStats._health));
            _eventBus.Invoke(new MedKitPerformedSignal());
            _medKitsCount--;
        }
    }

    public void OnMedKitPickUp() => _medKitsCount++;
}