using Cysharp.Threading.Tasks;
using FMODUnity;
using UnityEngine;
public class PlayerHealth : IPlayerHealth
{
    public PlayerHealth(PlayerStateMachine stateMachine)
    {
        _playerData = stateMachine;
        _maxHealth = _playerData._maxHealth;
        _playerData._health = _maxHealth;
        _playerMaterial = _playerData.TorsoSprite.material;
        RuntimeManager.StudioSystem.setParameterByName("Health", _playerData._health);
        RuntimeManager.StudioSystem.setParameterByName("Fight", 0);
    }

    private PlayerStateMachine _playerData;

    private Material _playerMaterial;

    private float _maxHealth;
    private int _medKitsCount;

    public Vector2 ApplyForce {  get; private set; } = Vector2.zero;
    public float ThrowTime {  get; private set; } = 0.15f;
    public bool OnDamageDelay { get; private set; } = false;


    public void ApplyDamage(float value, Vector2 applyForce, float throwTime)
    {
        if ((OnDamageDelay == true || _playerData.RollInput == true) || _playerData._health <= 0)
            return;

        _playerData.EventBus.Invoke(new ScreenShakeSignal(ScreenShakeBanks.PlayerGetDamage));

        //KKTS
        ComboSystem.Instance.TakeDamage();

        OnDamageDelay = true;
        ApplyForce = applyForce;
        ThrowTime = throwTime;
        _playerData._health = _playerData._immortality ? _playerData._health : _playerData._health -= value;

        _playerData.TorsoSprite.material = _playerData.VFXManager.PlayerDamageMaterial();
        _playerData.LegsSprite.material = _playerData.VFXManager.PlayerDamageMaterial();

        RuntimeManager.StudioSystem.setParameterByName("Health", _playerData._health);

        _playerData.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_PlayerBlood, _playerData.transform.position));

        _playerData.EventBus.Invoke(new PlayerHealthChangeSignal(_playerData._health));

        if(_playerData._health <= 0)
        {
            _playerData._health = 0;
            
            _playerData.TorsoSprite.material = _playerMaterial;
            _playerData.LegsSprite.material = _playerMaterial;

            _playerData.EventBus.Invoke(new PlayerOnDeathSignal());
            RuntimeManager.PlayOneShot("event:/SFX/PlayerSFX/Character Death");
            return;
        }
        RuntimeManager.PlayOneShot("event:/SFX/PlayerSFX/Character Hit");
        _playerData.EventBus.Invoke(new PlayerApplyForceSignal());
    }

    public async void TurnOffThrowDelay()
    {
        ApplyForce = Vector2.zero;
        ThrowTime = 0.15f;

        await TimeOfImmortality();
    }

    public async UniTask TimeOfImmortality()
    {
        await UniTask.Delay(_playerData.ImmortalityTime); // Задержка в 1 секунду

        _playerData.TorsoSprite.material = _playerMaterial;
        _playerData.LegsSprite.material = _playerMaterial;

        OnDamageDelay = false;
    }

    public void ImproveHealth()
    {
        if(_medKitsCount > 0 && _playerData._health < _maxHealth)
        {
            _playerData._health = _maxHealth;
            RuntimeManager.StudioSystem.setParameterByName("Health", _playerData._health);
            RuntimeManager.PlayOneShot("event:/SFX/LvlSFX/MedKit Use");
            _playerData.EventBus.Invoke(new PlayerHealthChangeSignal(_playerData._health));
            _playerData.EventBus.Invoke(new MedKitPerformedSignal());
            _medKitsCount--;
            _playerData.EventBus.Invoke(new SpawnParticlesSignal(ParticleBanks.p_Healing, _playerData.transform.position));
        }
    }
    public void OnMedKitPickUp()
    {
        RuntimeManager.PlayOneShot("event:/SFX/LvlSFX/MedKit PickUp");
        _medKitsCount++;
    }
}
