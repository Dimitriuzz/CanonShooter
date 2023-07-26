using UnityEngine;

namespace CannonShooter
{
    public class CSLevelController : LevelController
    {
        private int levelScore =3;
        private new void Start()
        {
            base.Start();
            Player.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);
            };

            EnemyWavesManager.Instance.TooManyEnemies += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);

            };

            EnemyWavesManager.OnAllWavesDead += () =>
            {
                LevelResultController.Instance.Show(true);
            };

            Player.Instance.OnTimeFinshed += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(true);
            };

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                
               
            }
            );

            void LifeScoreChange(int _)
            {
                levelScore -= 1;
                
                Player.Instance.OnLifeUpdate -= LifeScoreChange;
            }
            
            Player.Instance.OnLifeUpdate += LifeScoreChange;
            
            m_LevelTime += Time.time;
        }

        private void StopLevelActivity()
        {
            foreach (var AIController in FindObjectsOfType<AIController>())
            {
                AIController.ChangeNavigationState(true);
            }

            void DisableAll<T>() where T:MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<EnemyWavesManager>();
            DisableAll<EnemyWave>();
            DisableAll<Projectile>();
            DisableAll<NextWaveGUI>();
            EnemyWavesManager.Instance.currentWave.next = null;
            
        }
    }
}
