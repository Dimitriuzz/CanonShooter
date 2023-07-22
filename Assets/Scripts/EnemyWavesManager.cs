using System;
using UnityEngine;
using UnityEngine.UI;

namespace BallistaShooter
{
    public class EnemyWavesManager : MonoBehaviour
    {
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;
        //[SerializeField] Enemy m_EnemyPrefab;
        [SerializeField] private Enemy[] m_EnemysPrefab;
        [SerializeField] private int activeEnemyCount = 0;
        [SerializeField] private Text m_WaveText;
        [SerializeField] private Text m_EnemyCountText;


        private int m_WaveNumber = 0;
       
        public static event Action OnAllWavesDead;
        public static event Action EnemySpawned;

        public static event Action<Enemy> OnEnemySpawn;

        private void RecordEnemyDead() 
        {
            activeEnemyCount--;
            m_EnemyCountText.text = activeEnemyCount.ToString() + " врагов";
            if (activeEnemyCount == 0)
            {
                if (currentWave)
                {
                    ForceNextWave();
                }
                else
                {
                    OnAllWavesDead?.Invoke();
                }
            }
                
        }

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach ((Enemy EnemysPrefab, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        
                        var e = Instantiate(EnemysPrefab, paths[pathIndex].StartArea.RandomInsideZone,Quaternion.identity);
                        var a = e.GetComponent<Destructible>();
                        a.DeathEffectUse();
                        e.OnEnd += RecordEnemyDead;
                        //e.Use(asset);
                        e.GetComponent<TD_PatrolController>().SetPath(paths[pathIndex]);
                        OnEnemySpawn?.Invoke(e);
                        activeEnemyCount += 1;
                        
                    }


                }
            }
            EnemySpawned?.Invoke();
            currentWave = currentWave.PrepareNext(SpawnEnemies);
            m_EnemyCountText.text = activeEnemyCount.ToString() + " врагов";
            m_WaveNumber++;
            m_WaveText.text = m_WaveNumber.ToString() + " волна";
        }

        public void ForceNextWave()
        {
            if (currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());
                SpawnEnemies();
            }
                else
                {
                Debug.Log("no wave");
                if(activeEnemyCount==0)
                    OnAllWavesDead?.Invoke();
                Debug.Log("no wave invoked");
            }
            }
    }
}
