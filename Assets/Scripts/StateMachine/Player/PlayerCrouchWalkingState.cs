using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchWalkingState : PlayerBaseState
{
  public PlayerCrouchWalkingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
  {
  }

  public override void Enter()
  {
    Ctx.Animator.SetBool(Ctx.IsCrouchingHash, true);
    Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
    Ctx.Animator.SetBool(Ctx.IsCrouchingWalkingHash, true);
  }

  public override void Tick()
  {
    Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
    Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;
    CheckSwitchState();
  }


  public override void CheckSwitchState()
  {
    if (!Ctx.IsCrouchPressed)
    {
      SwitchState(Factory.Idle());
    }
    else if (!Ctx.IsMovementPressed && !Ctx.IsCrouchPressed)
    {
      SwitchState(Factory.Walk());
    }
    else if (!Ctx.IsMovementPressed)
    {
      SwitchState(Factory.Crouch());
    }
  }


  public override void Exit()
  {
    Ctx.Animator.SetBool(Ctx.IsCrouchingWalkingHash, false);
    Ctx.Animator.SetBool(Ctx.IsCrouchingHash, false);
    Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
  }

  public override void StartSubState()
  {

  }


}
