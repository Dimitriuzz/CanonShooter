
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    public class TextUpdate : MonoBehaviour
    {
        private Text m_Text;
        public enum UpdateSource { Gold, Life, Time }
        public UpdateSource source = UpdateSource.Gold;



        void Start()
        {
            m_Text = GetComponent<Text>();
            switch (source)
            {
                case UpdateSource.Gold:
                    Player.Instance.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    Player.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Time:
                    Player.Instance.TimeUpdateSubscribe(UpdateText);
                    break;
            }

        }

        private void OnDestroy()
        {
            switch (source)
            {
                case UpdateSource.Gold:
                    Player.Instance.OnGoldUpdate -= UpdateText;
                    break;
                case UpdateSource.Life:
                    Player.Instance.OnLifeUpdate -= UpdateText;
                    break;
                case UpdateSource.Time:
                    Player.Instance.OnTimeUpdate -= UpdateText;
                    break;
            }
        }

        private void UpdateText(int money)
        {
            m_Text.text = money.ToString();

        }

        private void Update()
        {
            if (source == UpdateSource.Time && int.Parse(m_Text.text) <= 5)
                m_Text.color = Color.red;
            else m_Text.color=new Color(255,247,184);
        }
    }
}