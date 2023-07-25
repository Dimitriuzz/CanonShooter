using System;
using UnityEngine;

namespace CannonShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Lifetime;  
        [SerializeField] public int m_Damage; 
        
        [SerializeField] private GameObject m_ImpactEffectPrefab;
        
        [SerializeField] private Sound m_FireSound = Sound.Arrow;
        [SerializeField] private Sound m_HitSound = Sound.ArrowHit;

        [SerializeField] private DamageType m_DamageType;
        
        private float m_Timer;
        

        private void Start()
        {
            
            m_FireSound.Play();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.root.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(m_Damage, m_DamageType);
                OnProjectileLifeEnd();
            }
        }

        private void Update()
        {
                    
            m_Timer += Time.deltaTime;
            
            if (m_Timer > m_Lifetime)
            {
                Destroy(gameObject);
            }
        }
        
        private void OnProjectileLifeEnd()
        {
            if (m_ImpactEffectPrefab != null)
            {
                Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
                m_HitSound.Play();
            }
            
            Destroy(gameObject);
        }
    }
}
