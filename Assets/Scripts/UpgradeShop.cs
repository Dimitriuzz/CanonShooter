
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    public class UpgradeShop : MonoBehaviour
    {
        /*[Serializable]
        private class UpgradeSlot
        {
            public BuyUpgrade slot;
            public UpgradeAsset upgrade;

            public void AssignUpgrade()
            {
                slot.SetUpgrade(upgrade);
            }
        }*/
        
        private int money;
        [SerializeField] private Text moneyText;
        [SerializeField] private BuyUpgrade[] sales;

        private void Start()
        {
            

            foreach (var slot in sales)
            {
                slot.Initialise();
                Debug.Log(slot.transform.name);
                slot.gameObject.GetComponentInChildren<Button>().onClick.AddListener(UpdateMoney);
                
            }

            UpdateMoney();
        }
        private void UpdateMoney()
        {
            money = MapCompletion.Instance.TotalScore;
            Debug.Log("money1" + money);
            money -= Upgrades.GetTotalCost();
            Debug.Log("money2" + money);
            moneyText.text = money.ToString();

            foreach (var slot in sales)
            {
                slot.CheckCost(money);
            }

        }



    }
}
