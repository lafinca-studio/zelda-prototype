using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
  public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
  public override void Enter()
  {
    IsRootState = true;
    handleJump();
  }
  public override void Tick()
  {
    CheckSwitchState();
    handleGravity();
  }
  public override void Exit()
  {
    Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
    if (Ctx.IsJumpPressed) {
      Ctx.RequireNewJumpPress = true;
    }
  }

  public override void StartSubState()
  {

  }
  public override void CheckSwitchState()
  {
    if (Ctx.CharacterController.isGrounded)
    {
      SwitchState(Factory.Grounded());
    }
  }

  void handleJump()
  {
    Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
    Ctx.IsJumping = true;
    Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
    Ctx.AppliedMovementY = Ctx.InitialJumpVelocity;
  }

  void handleGravity()
  {
    bool isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;
    float fallMultiplier = 2.0f;
    if (isFalling)
    {
      float previousYVelocity = Ctx.CurrentMovementY;
      Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * fallMultiplier * Time.deltaTime);
      Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, -10f);
    } else {
      float previousYVelocity = Ctx.CurrentMovementY;
      Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Time.deltaTime);
      Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovementY) * 0.5f;
    }
  }
}
