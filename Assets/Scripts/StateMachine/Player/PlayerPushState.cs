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

  void handlePush() {
    Ctx.Animator.SetBool(Ctx.IsPushingHash, true);
  }
}
