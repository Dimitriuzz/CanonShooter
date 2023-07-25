using System;
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    /// <summary>
    /// Класс сущности игрока. Содержит в себе все что связано с игроком.
    /// </summary>
    public class Player : MonoSingleton<Player>
    {

        [SerializeField] protected int m_NumLives;
        [SerializeField] private int m_Gold;
        public int Gold => m_Gold;

        private int m_LevelTime;
        public float m_CurrentTime { get; private set; }

        private float fireRate = 1;

        [SerializeField] private UpgradeAsset healthUpgrade;
        [SerializeField] private UpgradeAsset fireRateUpgrade;
        [SerializeField] private UpgradeAsset timeUpgrade;

        [SerializeField] private Text enemiesKilledText;
        
        private int bonusTime;

        #region Events
        public event Action OnPlayerDead;
        public event Action OnTimeFinshed;

        public event Action<int> OnGoldUpdate;
        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(m_Gold);
        }

        public event Action<int> OnTimeUpdate;
        public void TimeUpdateSubscribe(Action<int> act)
        {
            OnTimeUpdate += act;
            act(m_LevelTime);
        }
        
        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(m_NumLives);
        }
        #endregion


        protected override void Awake()
        {
            base.Awake();
            
            //var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            //TakeDamage(-level * 5);
            // if (Upgrades.GetUpgradeLevel(fireRateUpgrade)>0) fireRate = (1-Upgrades.GetUpgradeLevel(fireRateUpgrade)*0.1f);
            Debug.Log("Fire rate " + fireRate);
            // bonusTime = Upgrades.GetUpgradeLevel(timeUpgrade);
            Debug.Log("bonus time" + bonusTime);
            // bonusTime *= 5;
            Debug.Log("bonus time1" + bonusTime);
        }

        private void Start()
        {
            m_LevelTime = CSLevelController.Instance.ReferenceTime + bonusTime;
            m_CurrentTime = m_LevelTime;

        }

        private void Update()
        {
            if (m_CurrentTime <= 0) OnTimeFinshed?.Invoke();
            else m_CurrentTime -= Time.deltaTime;
            
            if (m_LevelTime - m_CurrentTime > 0.99)
            {
                m_LevelTime = (int)m_CurrentTime;
                if (m_LevelTime <= 0) m_LevelTime = 0;

                OnTimeUpdate?.Invoke(m_LevelTime);
            }

            

        }




        public void ChangeGold(int change)
        {
            m_Gold += change;
            OnGoldUpdate?.Invoke(m_Gold);
        }

        public void ChangeLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate?.Invoke(m_NumLives);
        }

        public void ChangeTime(int change)
        {
            OnTimeUpdate?.Invoke(m_LevelTime);
        }

        private void TakeDamage(int m_Damage)
        {
            m_NumLives -= m_Damage;
            
            if (m_NumLives <= 0)
            {
                m_NumLives = 0;
                OnPlayerDead?.Invoke();
            }
        }

        #region Score (current level only)

        public int Score { get; private set; }

        public int NumKills = 0;

        public void AddKill()
        {
            NumKills++;
            enemiesKilledText.text = "врагов убито: "+ NumKills.ToString();
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}