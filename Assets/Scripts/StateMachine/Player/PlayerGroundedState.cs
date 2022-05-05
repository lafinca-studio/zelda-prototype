using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
  public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
  {
    IsRootState = true;
  }
  public override void Enter()
  {
    StartSubState();
    Ctx.CurrentMovementY = Ctx.GroundedGravity;
    Ctx.AppliedMovementY = Ctx.GroundedGravity;
  }
  public override void Tick()
  {
    CheckSwitchState();
  }
  public override void Exit()
  {

  }

  public override void StartSubState()
  {
    if (!Ctx.IsMovementPressed)
    {
      SetSubState(Factory.Idle());
    }
    else if (Ctx.IsMovementPressed && !Ctx.IsPushing && !Ctx.IsCrouchPressed)
    {
      SetSubState(Factory.Walk());
    }
    else if (Ctx.IsCrouchPressed)
    {
      SetSubState(Factory.Crouch());
    }
  }
  public override void CheckSwitchState()
  {
    if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress && !Ctx.IsPushing)
    {
      SwitchState(Factory.Jump());
    }
  }
}
