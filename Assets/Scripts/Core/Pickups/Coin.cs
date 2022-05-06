using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda.Core
{
  public abstract class Coin : ScriptableObject
  {
    public abstract void AddMoney(GameObject target);

    public abstract void RemoveMoney(GameObject target);
  }

}

