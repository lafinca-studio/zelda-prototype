using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerBaseState
{
  public PlayerPushState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
  {
  }

  public override void Enter()
  {
    handlePush();
  }

  public override void Tick()
  {
    CheckSwitchState();
  }

  public override void Exit()
  {
    Ctx.Animator.SetBool(Ctx.IsPushingHash, false);
    Ctx.IsPushing = false;
    applyForce(0);
  }



  public override void StartSubState()
  {

  }

  public override void CheckSwitchState()
  {
    if (!Ctx.IsPushPressed || !Ctx.IsPushable)
    {
      SwitchState(Factory.Idle());
    }
  }

  void handlePush()
  {
    Ctx.Animator.SetBool(Ctx.IsPushingHash, true);
    Ctx.IsPushing = true;
    applyForce(2);
  }

  private void applyForce(float appliedForce)
  {
    foreach (PushBox arm in Ctx.PushForce)
    {
      arm._pushForce = appliedForce;
    }
  }
}
