using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
  public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
  public override void Enter()
  {
    Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
    Ctx.AppliedMovementX = 0;
    Ctx.AppliedMovementZ = 0;
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

  }
  public override void CheckSwitchState()
  {
    if (Ctx.IsMovementPressed) {
      SwitchState(Factory.Walk());
    }

    if (Ctx.IsPushPressed && Ctx.IsPushable) {
      SwitchState(Factory.Push());
    }

    if (Ctx.IsCrouchPressed) {
      SwitchState(Factory.Crouch());
    }
  }
}
