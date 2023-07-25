using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    public class Abilities : MonoBehaviour
    {

        [SerializeField] private float m_ReloadBonus;
        [SerializeField] private float m_SlowBonus;
        [SerializeField] private float m_StopBonus;
        [SerializeField] private int m_DamageBonus;
        
        [SerializeField] private int m_SlowBonusCost;
        [SerializeField] private int m_ReloadBonusCost;
        [SerializeField] private int m_StopBonusCost;
        [SerializeField] private int m_DamageBonusCost;

        [SerializeField] private Text m_StopBonusCostText;
        [SerializeField] private Text m_ReloadBonusCostText;
        [SerializeField] private Text m_DamageBonusCostText;

        private void Start()
        {
            m_StopBonusCostText.text = m_StopBonusCost.ToString();
            m_ReloadBonusCostText.text = m_ReloadBonusCost.ToString();
            m_DamageBonusCostText.text = m_DamageBonusCost.ToString();

        }


        public void DamageUpgrade()
        {
            if (Player.Instance.Gold >= m_DamageBonusCost)
            {
                
                Cannon.Instance.damageBonus+= m_DamageBonus;
                Player.Instance.ChangeGold(-m_DamageBonusCost);
                Debug.Log(Cannon.Instance.damageBonus);
            }

        }
        public void ReloadUpgrade()
        {
            Debug.Log("reload pressed");
            if (Player.Instance.Gold >= m_ReloadBonusCost)
            {
                Cannon.Instance.ChangeReloadTime(m_ReloadBonus);
                Player.Instance.ChangeGold(-m_ReloadBonusCost);
            }
        }

        public void UseStopBonus()
        {
            if (Player.Instance.Gold >= m_StopBonusCost)
            {
                void Stop(Enemy enemy)
                {
                    enemy.GetComponent<AIController>().ChangeNavigationState(false);
                }

                void GroupStop(bool slow)
                {
                    foreach (var destructible in Destructible.AllDestructibles)
                    {
                        if(destructible.TryGetComponent<AIController>(out var enemy))
                            enemy.ChangeNavigationState(slow);
                    }
                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_StopBonus);
                    
                    GroupStop(false);

                    EnemyWavesManager.OnEnemySpawn -= Stop;
                }

                GroupStop(true);

                EnemyWavesManager.OnEnemySpawn += Stop;
                StartCoroutine(Restore());
                Player.Instance.ChangeGold(-m_StopBonusCost);
            }
        }

        public void UseSlowBonus()
        {
            if (Player.Instance.Gold >= m_SlowBonusCost)
            {
                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<AIController>().SetSlowed(true);
                }

                void ChangeGroupSlow(bool slow)
                {
                    foreach (var destructible in Destructible.AllDestructibles)
                    {
                        if(destructible.TryGetComponent<AIController>(out var enemy))
                            enemy.SetSlowed(slow);
                    }
                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_SlowBonus);
                    
                    ChangeGroupSlow(false);

                    EnemyWavesManager.OnEnemySpawn -= Slow;
                }

                ChangeGroupSlow(true);

                EnemyWavesManager.OnEnemySpawn += Slow;
                StartCoroutine(Restore());
                Player.Instance.ChangeGold(-m_ReloadBonusCost);
            }
        }
    }
}
