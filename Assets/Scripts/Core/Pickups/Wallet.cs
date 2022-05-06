using UnityEngine;
using TMPro;

namespace Zelda.Core
{
  public class Wallet : MonoBehaviour
  {
    [SerializeField] int coins = 0;
    public TextMeshProUGUI walletUI;
    private bool updateWallet = false;

    public int Coins { get { return coins; } set { coins = value; } }
    public bool UpdateWallet { get { return updateWallet; } set { updateWallet = value; } }


    private void Update() {
      if (updateWallet) {
        walletUI.text = coins.ToString();
        updateWallet = false;
      }
    }
  }
}