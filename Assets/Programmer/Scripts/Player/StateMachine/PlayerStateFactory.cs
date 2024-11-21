using System.Collections.Generic;

public enum PlayerStates
{
    grounded,
    fall,
    jump, 
    jumpFromStairs,
    idle,
    run,
    fallingRun,
    crouch,
    roll,
    onStairs,
    onDeath
}

public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;

        _states[PlayerStates.grounded] = new PlayerGroundedState(_context, this);
        _states[PlayerStates.fall] = new PlayerFallState(_context, this);
        _states[PlayerStates.jump] = new PlayerJumpState(_context, this);
        _states[PlayerStates.jumpFromStairs] = new PlayerJumpFromStairsState(_context, this);
        _states[PlayerStates.idle] = new PlayerIdleState(_context, this);
        _states[PlayerStates.run] = new PlayerRunState(_context, this);
        _states[PlayerStates.fallingRun] = new PlayerFallingRunState(_context, this);
        _states[PlayerStates.crouch] = new PlayerCrouchState(_context, this);
        _states[PlayerStates.roll] = new PlayerRollState(_context, this);
        _states[PlayerStates.onStairs] = new PlayerOnStairsState(_context, this);
        _states[PlayerStates.onDeath] = new PlayerOnDeathState(_context, this);
    }

    public PlayerBaseState Grounded()
    {
        return _states[PlayerStates.grounded];
    }

    public PlayerBaseState Jump()
    {
        return _states[PlayerStates.jump];
    }
    public PlayerBaseState JumpFromStairs()
    {
        return _states[PlayerStates.jumpFromStairs];
    }
    public PlayerBaseState Idle()
    {
        return _states[PlayerStates.idle];
    }
    public PlayerBaseState Crouch()
    {
        return _states[PlayerStates.crouch];
    }
    public PlayerBaseState Roll()
    {
        return _states[PlayerStates.roll];
    }
    public PlayerBaseState Fall()
    {
        return _states[PlayerStates.fall];
    }
    public PlayerBaseState Run()
    {
        return _states[PlayerStates.run];
    }
    public PlayerBaseState FallingRun()
    {
        return _states[PlayerStates.fallingRun];
    }
    public PlayerBaseState OnStairs()
    {
        return _states[PlayerStates.onStairs];
    }
    public PlayerBaseState OnDeath()
    {
        return _states[PlayerStates.onDeath];
    }
}
