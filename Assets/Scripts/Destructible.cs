using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CannonShooter;

namespace CannonShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То что может иметь хит поинты.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        [SerializeField] private GameObject remainsPrefab;

        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Стартовое кол-во хитпоинтов.
        /// </summary>
        [SerializeField] protected int m_HitPoints;

        [SerializeField] GameObject deathEffect;

        /// <summary>
        /// Текущие хит поинты
        /// </summary>
        protected int m_CurrentHitPoints;

        public int CurrentHitPoints => m_CurrentHitPoints;

        public int MaxtHitPoints => m_HitPoints;

        /// <summary>
        /// Кол-во очков за уничтожение.
        /// </summary>
        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;
        
        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private ImpactEffect m_ExplosionPrefab;

        #endregion

        #region Unity events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        #region Безтеговая коллекция скриптов на сцене

        private static HashSet<Destructible> m_AllDestructibles;


        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;


        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        #endregion

        #endregion

        #region Public API

        /// <summary>
        /// Применение дамага к объекту.
        /// </summary>
        /// <param name="damage"></param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible || damage <= 0)
                return;

            m_CurrentHitPoints -= damage;
            Debug.Log(damage + " applied");

            if (m_CurrentHitPoints <= 0)
                OnDeath();
        }

        public void AddHitPoints(int hp)
        {
            m_CurrentHitPoints = Mathf.Clamp(m_CurrentHitPoints + hp, 0, m_HitPoints);
        }

        #endregion

        /// <summary>
        /// Перепоределяемое событие уничтожения объекта, когда хит поинты ниже нуля.
        /// </summary>
        protected virtual void OnDeath()
        {
            if(m_ExplosionPrefab != null)
            {
                var explosion = Instantiate(m_ExplosionPrefab.gameObject);
                explosion.transform.position = transform.position;
            }

            DeathEffectUse();

            CameraShake.Instance.ShakeCamera();

            if (remainsPrefab != null)
            {
                Instantiate(remainsPrefab, transform.position, Quaternion.identity);
            }
            
            m_EventOnDeath?.Invoke();

            Player.Instance.AddKill();
            
            Destroy(gameObject);
        }

        public void DeathEffectUse()
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            }
        }
    }

}