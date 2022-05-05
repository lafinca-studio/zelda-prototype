using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
  public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
  {
  }

  public override void Enter()
  {
    Ctx.Animator.SetBool(Ctx.IsCrouchingHash, true);
    Ctx.AppliedMovementX = 0;
    Ctx.AppliedMovementZ = 0;
  }

  public override void Tick()
  {
    CheckSwitchState();
  }

  public override void CheckSwitchState()
  {
    if (!Ctx.IsCrouchPressed && !Ctx.IsMovementPressed)
    {
      SwitchState(Factory.Idle());
    }
    else if (Ctx.IsCrouchPressed && Ctx.IsMovementPressed)
    {
      SwitchState(Factory.CrouchWalk());
    }
  }

  public override void Exit()
  {
    Ctx.Animator.SetBool(Ctx.IsCrouchingHash, false);
  }

  public override void StartSubState()
  {

  }

}
