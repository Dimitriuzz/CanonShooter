using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BallistaShooter;

namespace BallistaShooter
{

    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;  
        [SerializeField] private float m_Lifetime;  
        [SerializeField] private int m_Damage;  
        [SerializeField] private GameObject m_ImpactEffectPrefab;
        [SerializeField] private Sound m_FireSound=Sound.Arrow;
        [SerializeField] private Sound m_HitSound=Sound.ArrowHit;
        private float m_Timer;
        private RaycastHit hit;

        public enum DamageType { Piercing = 0, Fire = 1, Ice = 2 }
        [SerializeField] private DamageType m_DamageType;

        private void Start()
        {
            m_FireSound.Play();
        }

        private void Update()
        {
            //float stepLength = Time.deltaTime * m_Velocity;
            //Vector2 step = transform.up * stepLength;
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
            //Debug.Log("transform "+transform.position+" ray " +ray);
            
            if (Physics.Raycast(ray, out hit, 10))
            {
                var destructible = hit.collider.transform.root.GetComponent<Destructible>();
                //Debug.Log("hit the target");

                if (destructible != null && destructible != m_Parent)
                {
                    //destructible.ApplyDamage(m_Damage);
                   // var anim = destructible.GetComponent<Animator>();
                   // anim.SetInteger("State",1);
                    
                    var enemy = destructible.GetComponent<Enemy>();
                    //enemy.isHit = true;
                    //enemy.Selection.gameObject.SetActive(false);
                    enemy.TakeDamage(m_Damage, m_DamageType);
                    Destroy(gameObject);

                    //Debug.Log("damage " + m_Damage);
                    // }
                    /*if(m_Parent==Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                    }*/
                }
                //OnProjectileLifeEnd(hit.collider, hit.point);

                if (m_ImpactEffectPrefab != null)
                {
                    var m_Impact= Instantiate(m_ImpactEffectPrefab);
                    m_Impact.transform.position = transform.position;
                    m_Impact.transform.up = transform.up;
                    m_HitSound.Play();
                    
                    
                }
            }

            

            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
            {
                /*if (m_ImpactEffectPrefab != null)
                {
                    Instantiate(m_ImpactEffectPrefab, new Vector3(transform.position.x, transform.position.y, 0).normalized, Quaternion.identity);
                }*/
                Destroy(gameObject);
            }
           /* if (transform.tag == "Rocket")
            {
                RocketMovement();
            }
            else
            {
                transform.position += new Vector3(step.x, step.y, 0);
            }*/
        }

        void OnHit(RaycastHit2D hit)
        {
            //m_HitSound.Play();
            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage, m_DamageType);


            }
        }

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {

            Destroy(gameObject);
        }
        private Destructible m_Parent;
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    private void RocketMovement()
        {
            Collider2D[] hit;
            hit = Physics2D.OverlapCircleAll(transform.position, m_Velocity*m_Lifetime);
            //Debug.Log("Range :" + m_Velocity * m_Lifetime);
            float m_MinDistance=10000000;
            float m_CurrentDistance;
            int m_nearestIndex=0;
            for (int i = 0; i < hit.Length; i++)
            {
                //Debug.Log("Got :" + hit[i].transform.root.name);
                Destructible dest = hit[i].transform.root.GetComponent<Destructible>();
            
                if (dest != null&&dest!=m_Parent)
                {
                    m_CurrentDistance = (dest.transform.position - transform.position).magnitude;
                    if (m_CurrentDistance < m_MinDistance)
                    {
                        m_MinDistance = m_CurrentDistance;
                        m_nearestIndex = i;
                    }

                    //Debug.Log("Hit :" + hit[i].transform.root.name+" distance :"+m_CurrentDistance+" coord"+hit[i].transform.position);
                }
            }
            //Debug.Log("Nearest :" + hit[m_nearestIndex].transform.root.name + " coord" + hit[m_nearestIndex].transform.position);
            if (hit[m_nearestIndex] != null && hit[m_nearestIndex] != m_Parent)
            {
                transform.up = hit[m_nearestIndex].transform.position - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, hit[m_nearestIndex].transform.position, m_Velocity * Time.deltaTime);
            }
            else
            {
                float stepLength = Time.deltaTime * m_Velocity;
                Vector2 step = transform.up * stepLength;
                transform.position += new Vector3(step.x, step.y, 0);
            }

        }
    }


}
