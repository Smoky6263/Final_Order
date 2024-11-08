public abstract class PlayerBaseState
{

    private PlayerStateMachine _context;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;
    
    private bool _isRootState = false;
    protected bool IsRootState { set { _isRootState = value; } }
    protected PlayerStateMachine Context { get { return _context; } }
    protected PlayerStateFactory Factory { get { return _factory; } }
    public PlayerBaseState CurrentSuperState { get { return _currentSuperState; } }
    public  PlayerBaseState CurrentSubState { get { return _currentSubState; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _context = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();

    public void UpdateStates() 
    {
        UpdateState();
        if(_currentSubState != null)
            _currentSubState.UpdateStates();
    }

    public void ExitStates()
    {
        ExitState();
        if(_currentSubState != null)
            _currentSubState.ExitStates();
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        //current state exists state
        ExitState();

        //new state enters state
        newState.EnterState();

        if (_isRootState)
        {
            //switch current state of context
            _context.CurrentState = newState;
        }
        else if(_currentSuperState != null)
        {
            //set the current super states sub state to the new state
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState; 
    }
}
