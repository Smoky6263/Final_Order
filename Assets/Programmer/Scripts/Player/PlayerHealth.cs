using Cinemachine;
using FMODUnity;
using UnityEngine;
public class PlayerHealth : IPlayerHealth
{
    public PlayerHealth(PlayerStateMachine stateMachine)
    {
        _playerData = stateMachine;
        _eventBus = _playerData.EventBus;
        _maxHealth = _playerData._maxHealth;
        _playerData._health = _maxHealth;
        _impulseSource = _playerData._impulseSource;
        _profile = _playerData._profile;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Health", _playerData._health);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Fight", 0);
    }

    private EventBus _eventBus;
    private PlayerStateMachine _playerData;

    private float _maxHealth;
    private int _medKitsCount;
    private CinemachineImpulseSource _impulseSource;
    private ScreenShakeProfile _profile;

    public Vector2 ApplyForce {  get; private set; } = Vector2.zero;
    public bool OnDamageDelay { get; private set; } = false;


    public void GetDamage(float value, Vector2 applyForce)
    {
        if ((OnDamageDelay == true || _playerData.RollInput == true) || _playerData._health <= 0)
            return;

        OnDamageDelay = true;
        ApplyForce = applyForce;
        _playerData._health -= value;

        CameraShakeManager.instance.ScreenShakeFromProfile(_profile, _impulseSource);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Health", _playerData._health);

        _playerData.VFXManager.SpawnBloodParticles(_playerData.transform.position, _playerData.VFXManager.PlayerBlood);

        _eventBus.Invoke(new PlayerHealthChangeSignal(_playerData._health));

        if(_playerData._health <= 0)
        {
            _playerData._health = 0;

            _eventBus.Invoke(new PlayerOnDeathSignal());
            RuntimeManager.PlayOneShot("event:/SFX/Character Death");
            return;
        }
        RuntimeManager.PlayOneShot("event:/SFX/Character Hit");
        _eventBus.Invoke(new PlayerApplyForceSignal());
    }

    public void TurnOffDamageDelay()
    {
        ApplyForce = Vector2.zero;
        OnDamageDelay = false;
    }

    public void ImproveHealth()
    {
        if(_medKitsCount > 0 && _playerData._health < _maxHealth)
        {
            _playerData._health = _maxHealth;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Health", _playerData._health);
            RuntimeManager.PlayOneShot("event:/SFX/MedKit Use");
            _eventBus.Invoke(new PlayerHealthChangeSignal(_playerData._health));
            _eventBus.Invoke(new MedKitPerformedSignal());
            _medKitsCount--;
            _playerData.VFXManager.SpawnHealParticles(_playerData.transform.position);
        }
    }
    public void OnMedKitPickUp()
    {
        RuntimeManager.PlayOneShot("event:/SFX/MedKit PickUp");
        _medKitsCount++;
    }
}
