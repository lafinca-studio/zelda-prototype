using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
  public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
  public override void Enter()
  {
    Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
  }
  public override void Tick()
  {
    Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
    Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;
    CheckSwitchState();
  }

  public override void CheckSwitchState()
  {
    if (!Ctx.IsMovementPressed)
    {
      SwitchState(Factory.Idle());
    }

    if (Ctx.IsCrouchPressed && Ctx.IsMovementPressed) {
      SwitchState(Factory.CrouchWalk());
    }
  }
  public override void Exit()
  {
    Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
  }

  public override void StartSubState()
  {

  }

}
