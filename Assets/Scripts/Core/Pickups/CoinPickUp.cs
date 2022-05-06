using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda.Core
{
  [CreateAssetMenu(menuName = "Coins")]

  public class CoinPickUp : Coin
  {
    [SerializeField] int coinValue = 1;
    

    public override void AddMoney(GameObject target)
    {
      Wallet wallet = target.GetComponent<Wallet>();
      wallet.Coins += coinValue;
      wallet.UpdateWallet = true;
    }

    public override void RemoveMoney(GameObject target)
    {

    }

  }

}

