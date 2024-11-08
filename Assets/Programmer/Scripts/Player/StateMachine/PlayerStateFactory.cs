public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Crouch()
    {
        return new PlayerCrouchState(_context, this);
    }
    public PlayerBaseState Roll()
    {
        return new PlayerRollState(_context, this);
    }
    public PlayerBaseState Fall()
    {
        return new PlayerFallState(_context, this);
    }
    public PlayerBaseState Run()
    {
        return new PlayerRunState(_context, this);
    }
    public PlayerBaseState FallingRun()
    {
        return new PlayerFallingRunState(_context, this);
    }
    public PlayerBaseState OnStairs()
    {
        return new PlayerOnStairsState(_context, this);
    }
}
