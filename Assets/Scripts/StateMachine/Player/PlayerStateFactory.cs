using System.Collections.Generic;

public class PlayerStateFactory
{
  enum PlayerStates
  {
    idle,
    walk,
    grounded,
    jump,
    push,
    crouch,
    crouchWalk
  }
  PlayerStateMachine _context;
  Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

  public PlayerStateFactory(PlayerStateMachine currentContext)
  {
    _context = currentContext;
    _states[PlayerStates.idle] = new PlayerIdleState(_context, this);
    _states[PlayerStates.walk] = new PlayerWalkState(_context, this);
    _states[PlayerStates.jump] = new PlayerJumpState(_context, this);
    _states[PlayerStates.grounded] = new PlayerGroundedState(_context, this);
    _states[PlayerStates.push] = new PlayerPushState(_context, this);
    _states[PlayerStates.crouch] = new PlayerCrouchState(_context, this);
    _states[PlayerStates.crouchWalk] = new PlayerCrouchWalkingState(_context, this);
  }

  public PlayerBaseState Idle()
  {
    return _states[PlayerStates.idle];
  }

  public PlayerBaseState Walk()
  {
    return _states[PlayerStates.walk];
  }

  public PlayerBaseState Jump()
  {
    return _states[PlayerStates.jump];
  }

  public PlayerBaseState Grounded()
  {
    return _states[PlayerStates.grounded];
  }

  public PlayerBaseState Push()
  {
    return _states[PlayerStates.push];
  }

  public PlayerBaseState Crouch()
  {
    return _states[PlayerStates.crouch];
  }

  public PlayerBaseState CrouchWalk()
  {
    return _states[PlayerStates.crouchWalk];
  }
}
