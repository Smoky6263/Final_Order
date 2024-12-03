using Cysharp.Threading.Tasks;
using UnityEditor.Rendering.LookDev;
public class PlayerHealth : IPlayerHealth
{
    public PlayerHealth(PlayerStateMachine stateMachine)
    {
        _playerData = stateMachine;
        _eventBus = _playerData.EventBus;
        _maxHealth = _playerData._maxHealth;
        _playerData._health = _maxHealth;
    }

    private EventBus _eventBus;
    private PlayerStateMachine _playerData;

    private float _maxHealth;
    private int _medKitsCount;
    public bool OnDamageDelay { get; private set; } = false;


    public async void GetDamage(float value)
    {
        if (OnDamageDelay == true && _playerData.RollInput == false)
            return;

        OnDamageDelay = true;
        _playerData._health -= value;
        _playerData.VFXManager.SpawnBloodParticles(_playerData.transform.position, _playerData.VFXManager.PlayerBlood);
        if(_playerData._health <= 0)
        {
            _playerData._health = 0;
            _eventBus.Invoke(new PlayerOnDeathSignal());
        }

        _eventBus.Invoke(new PlayerHealthChangeSignal(_playerData._health));
        await DamageDelayTask();
    }

    private async UniTask DamageDelayTask()
    {
        await UniTask.Delay(_playerData.DamageDelayTime);
        OnDamageDelay = false;
    }

    public void ImproveHealth()
    {
        if(_medKitsCount > 0 && _playerData._health < _maxHealth)
        {
            _playerData._health = _maxHealth;
            _eventBus.Invoke(new PlayerHealthChangeSignal(_playerData._health));
            _eventBus.Invoke(new MedKitPerformedSignal());
            _medKitsCount--;
        }
    }
    public void OnMedKitPickUp() => _medKitsCount++;
}
