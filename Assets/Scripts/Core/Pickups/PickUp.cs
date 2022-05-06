using UnityEngine;

namespace Zelda.Core
{
  public class PickUp : MonoBehaviour
  {
    public CoinPickUp coin;
    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.tag == "Player")
      {
        Destroy(gameObject);
        coin.AddMoney(other.gameObject);
      }
    }

  }
  
}