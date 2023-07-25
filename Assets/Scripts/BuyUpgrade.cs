
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text level, cost;
        [SerializeField] private Button buyButton;
        public Button BuyButton => buyButton;
        [SerializeField] private UpgradeAsset asset;
        private int costNumber = 0;

        public void Initialise()
        {
            Debug.Log(asset.UpgradeName);
            upgradeIcon.sprite = asset.sprite;
            var savedlevel = Upgrades.GetUpgradeLevel(asset);
            
            if (savedlevel >= asset.costByLevel.Length)
            {
                buyButton.interactable = false;
                buyButton.transform.Find("Star").gameObject.SetActive(false);
                buyButton.transform.Find("Text").gameObject.SetActive(false);
                cost.text = "X";
                level.text = $"Lvl: { savedlevel + 1} (Max)";
                costNumber = int.MaxValue;

            }
            else
            { 
                level.text = $"Lvl: { savedlevel + 1}";
                costNumber = asset.costByLevel[savedlevel];
                cost.text = costNumber.ToString();
               
            }
        }

        public void CheckCost(int money)
        {
            buyButton.interactable = money >= costNumber;

        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialise();
        }

    }
}
