
using UnityEngine;
using UnityEngine.UI;

namespace BallistaShooter
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_Ta;
        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform buildSite;
       
        public void SetBuildSite (Transform value)
        {
            buildSite = value;
        }

       
        private void Start()
        {
            TDPlayer.Instance.GoldUpdateSubscribe(GoldStatusCheck);
            m_Text.text = m_Ta.goldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_Ta.GUISprite;
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold >=m_Ta.goldCost !=m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.white : Color.red;

            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_Ta, buildSite);
            BuildSite.HideControls();
        }

    }
}