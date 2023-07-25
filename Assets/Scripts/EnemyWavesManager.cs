using System;
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    public class EnemyWavesManager : MonoSingleton<EnemyWavesManager>
    {
        [SerializeField] private Path[] paths;
        [SerializeField] public EnemyWave currentWave;
        [SerializeField] private int activeEnemyCount;
       
        public static event Action OnAllWavesDead;
        public static event Action OnWaveSpawned;
        
        public static event Action<Enemy> OnEnemySpawn;

        [SerializeField] private Text m_WaveText;
        [SerializeField] private Text m_EnemyCountText;

        private int m_WaveNumber = 0;

        public event Action TooManyEnemies;

        private void RecordEnemyDead() 
        {
            activeEnemyCount--;
            m_EnemyCountText.text = "врагов: "+ activeEnemyCount.ToString();
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
            foreach ((Enemy EnemyPrefab, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(EnemyPrefab,
                                             paths[pathIndex].StartArea.RandomInsideZone(),
                                                    paths[pathIndex].StartArea.transform.rotation);
                        var a = e.GetComponent<Destructible>();
                        a.DeathEffectUse();
                        e.OnEnd += RecordEnemyDead;

                        e.GetComponent<AIController>().SetPath(paths[pathIndex]);
                        activeEnemyCount += 1;
                        OnEnemySpawn?.Invoke(e);
                        if (activeEnemyCount >= 10) TooManyEnemies?.Invoke();
                    }
                }
            }
            OnWaveSpawned?.Invoke();
            currentWave = currentWave.PrepareNext(SpawnEnemies);
            m_EnemyCountText.text = "врагов: " + activeEnemyCount.ToString();
            if (activeEnemyCount <= 3) m_EnemyCountText.color = Color.green;
            if (activeEnemyCount >= 8) m_EnemyCountText.color = Color.red;
            if (activeEnemyCount > 3&& activeEnemyCount<=7) m_EnemyCountText.color = Color.yellow;

            m_WaveNumber++;
            m_WaveText.text = m_WaveNumber.ToString() + " волна";
        }

        public void ForceNextWave()
        {
            if (currentWave)
            {
                Player.Instance.ChangeGold((int)currentWave.GetRemainingTime());
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
