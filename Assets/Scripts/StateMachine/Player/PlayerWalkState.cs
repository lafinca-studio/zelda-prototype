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
    CheckSwitchState();
    Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
    Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;
  }
  public override void Exit()
  {

  }

  public override void StartSubState()
  {

  }
  public override void CheckSwitchState()
  {
    if (!Ctx.IsMovementPressed) {
      SwitchState(Factory.Idle());
    }
  }
}
