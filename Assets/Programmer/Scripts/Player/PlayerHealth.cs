using Cysharp.Threading.Tasks;

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
    public bool OnDamageDelay { get; private set; } = false;


    public async void TakeDamage(float value)
    {
        if (OnDamageDelay == true && _playerStats.RollInput == false)
            return;

        OnDamageDelay = true;
        _playerStats._health -= value;

        if(_playerStats._health < 0 )
            _playerStats._health = 0;

        _eventBus.Invoke(new PlayerHealthChangeSignal(_playerStats._health));
        await DamageDelayTask();
    }
    private async UniTask DamageDelayTask()
    {
        await UniTask.Delay(_playerStats.DamageDelayTime);
        OnDamageDelay = false;
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
