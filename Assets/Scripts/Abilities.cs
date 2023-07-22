using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallistaShooter
{
    public class Abilities : MonoBehaviour
    {
        private TDPlayer m_Player;
        private Shoot m_Shoot;
        [SerializeField] private float m_ReloadBonus;
        [SerializeField] private float m_SlowBonus;
        [SerializeField] private int m_SlowBonusCost;
        [SerializeField] private int m_ReloadBonusCost;
        private void Start()
        {
            m_Player = GetComponent<TDPlayer>();
            m_Shoot = GetComponent<Shoot>();
        }

        public void ReloadUpgrade()
        {
            Debug.Log("reload pressed");
            if (m_Player.m_Gold >= m_ReloadBonusCost)
            {
                m_Shoot.ChangeReloadTime(m_ReloadBonus);
                m_Player.ChangeGold(m_ReloadBonusCost);
            }
        }

        public void UseSlowBonus()
        {
            if (m_Player.m_Gold >= m_SlowBonusCost)
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().StopMovement();

                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_SlowBonus);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.ContinueMovement();
                    EnemyWavesManager.OnEnemySpawn -= Slow;
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.StopMovement();

                EnemyWavesManager.OnEnemySpawn += Slow;
                StartCoroutine(Restore());
                m_Player.ChangeGold(m_ReloadBonusCost);
            }

        }

    }
}
