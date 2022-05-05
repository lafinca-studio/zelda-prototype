public abstract class PlayerBaseState
{
  private bool _isRootState = false;
  private PlayerStateMachine _ctx;
  private PlayerStateFactory _factory;
  private PlayerBaseState _currentSubState;
  private PlayerBaseState _currentSuperState;

  protected bool IsRootState { set { _isRootState = value; } }
  protected PlayerStateMachine Ctx { get { return _ctx; } }
  protected PlayerStateFactory Factory { get { return _factory; } }

  public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
  {
    _ctx = currentContext;
    _factory = playerStateFactory;
  }

  public abstract void Enter();

  public abstract void Tick();

  public abstract void Exit();
  public abstract void CheckSwitchState();
  public abstract void StartSubState();

  public void UpdateStates()
  {
    Tick();
    if (_currentSubState != null)
    {
      _currentSubState.UpdateStates();
    }
  }
  protected void SwitchState(PlayerBaseState newState)
  {
    Exit();

    newState.Enter();

    if (_isRootState)
    {
      _ctx.CurrentState = newState;
    }
    else if (_currentSuperState != null)
    {
      _currentSuperState.SetSubState(newState);
    }
  }

  protected void SetSuperState(PlayerBaseState newSuperState)
  {
    _currentSuperState = newSuperState;
  }
  protected void SetSubState(PlayerBaseState newSubState)
  {
    _currentSubState = newSubState;
    newSubState.SetSuperState(this);
  }
}
