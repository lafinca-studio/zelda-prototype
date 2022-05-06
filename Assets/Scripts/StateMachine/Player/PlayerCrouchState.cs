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
    Ctx.CharacterController.height = 0.5f;
    Ctx.CharacterController.center = new Vector3(0, 0.3f, 0);
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
    Ctx.CharacterController.height = 1.6f;
    Ctx.CharacterController.center = new Vector3(0, 0.8f, 0);
  }

  public override void StartSubState()
  {

  }

}
