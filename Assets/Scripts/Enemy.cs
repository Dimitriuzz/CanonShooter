
using UnityEngine;
using System;
using UnityEngine.UI;

namespace BallistaShooter
{
    [RequireComponent(typeof(TD_PatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_Gold = 1;
        [SerializeField] private int m_Armor = 0;

        [SerializeField] public Transform Aim;
        [SerializeField] public Transform Selection;
        [SerializeField] public GameObject Tower;
        [SerializeField] private  Image ProgressBar;

        private Animator animator;
        private string animationName = "Damaged";

        public bool isHit;

        public enum ArmorType { Piercing = 0, Fire = 1, Ice = 2 };

        [SerializeField] private ArmorType m_ArmorType;

        private Destructible m_destructible;

        private int CalculateDamage(int power, int damagetype, int armortype, int armor)
        {

            if (damagetype == armortype) return 0;
            else return power;

        }




        private void Awake()
        {
            m_destructible = GetComponent<Destructible>();
            animator = GetComponent<Animator>();
        }

        public int EnemyDamage => m_Damage;

        public event Action OnEnd;

        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        private void Update()
        {
            if (ProgressBar != null)
            {
                float progress = (float)m_destructible.CurrentHitPoints / (float) m_destructible.MaxtHitPoints;
                ProgressBar.fillAmount = progress;

                //Debug.Log("cur HP " + m_destructible.CurrentHitPoints + "max HP " + m_destructible.MaxtHitPoints);
                //Debug.Log("HP " + progress.ToString());
            }
            //transform.Translate((transform.position-Tower.transform.position) * 1 * Time.deltaTime);
        }
        public void Use(EnemyAsset asset)
        {
            //var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
            //sr.color = asset.color;
            //sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);
           // sr.GetComponent<Animator>().runtimeAnimatorController = asset.animation;

            GetComponent<SpaceShip>().Use(asset);

           // GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_Damage = asset.damage;
            m_Gold = asset.gold;
        }

        public void TakeDamage(int damage, Projectile.DamageType damageType)
        {
            m_destructible.ApplyDamage(CalculateDamage(damage, (int)m_ArmorType, (int)damageType, m_Armor));
            animator.Play(animationName);
        }
        public void DamagePlayer()
        {
            TDPlayer.Instance.ChangeLife(m_Damage);
            animator.Play("Attacks");
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
    }
}
