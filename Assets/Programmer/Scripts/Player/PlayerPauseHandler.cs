using UnityEngine;

public class PlayerPauseHandler : MonoBehaviour, IPauseHandler
{
    private PlayerStateMachine _playerStateMachine;
    private Rigidbody2D _rigidbody2D;
    private PlayerAnimatorController _playerAnimatorController;
    private CharacterController _characterController;

    private PauseManager _pauseManager;

    public void Init(PauseManager pauseManager)
    {
        
    }

    private void Start()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimatorController = GetComponent<PlayerAnimatorController>();
        _characterController = GetComponent<CharacterController>();

        _pauseManager = _playerStateMachine.PauseManager;
        _pauseManager.Register(this);
    }

    public void SetPlay()
    {
        _playerStateMachine.OnPause = false;
        _rigidbody2D.simulated = true;
        _playerAnimatorController.SetPlay();

        if (_playerStateMachine.OnCutScene) return;
        _characterController.enabled = true;
    }

    public void SetPause()
    {
        _playerStateMachine.OnPause = true;
        _rigidbody2D.simulated = false;
        _playerAnimatorController.SetPause();

        if (_playerStateMachine.OnCutScene) return;
        _characterController.enabled = false;
    }

    public void OnDestroy() => Unregister();

    public void Unregister() => _pauseManager.Unregister(this);

    
}
