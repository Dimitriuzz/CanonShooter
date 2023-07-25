using System;
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    public class Enemy : Destructible
    {
        [SerializeField] private int m_Damage = 1;
        public int EnemyDamage => m_Damage;
        
        [SerializeField] private int m_Gold = 1;
        
        [SerializeField] private int m_Armor = 0;
        [SerializeField] private ArmorType m_ArmorType;

        [SerializeField] private Image HPBar;
        [SerializeField] private Text HPAmount;

        private Animator animator;
        private string animationName = "Damaged";
        
        public event Action OnEnd;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (HPBar == null) return;
            
            float progress = CurrentHitPoints / (float)MaxtHitPoints;
            HPBar.fillAmount = progress;
            HPAmount.text = CurrentHitPoints.ToString();
        }

        protected override void OnDestroy()
        {
            OnEnd?.Invoke();
            base.OnDestroy();
        }

        protected override void OnDeath()
        {
            GivePlayerGold();
            base.OnDeath();
        }
        
        public void TakeDamage(int damage, DamageType damageType)
        {
            ApplyDamage(CalculateDamage(damage, (int)m_ArmorType, (int)damageType, m_Armor));
            animator.Play(animationName);
        }

        private int CalculateDamage(int power, int damageType, int armorType, int armor)
        {
            return damageType == armorType ? 0 : power;
        }

        public void DamagePlayer()
        {
            Player.Instance.ChangeLife(m_Damage);
            animator.Play("Attacks");
        }

        public void GivePlayerGold()
        {
            Player.Instance.ChangeGold(m_Gold);
        }
    }
}

