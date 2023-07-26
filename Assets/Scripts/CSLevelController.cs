using UnityEngine;

namespace CannonShooter
{
    public class CSLevelController : LevelController
    {

        

        //public float ReferenceTime { get; internal set; }
        
        private new void Start()
        {
            base.Start();

            //ReferenceTime = m_ReferenceTime;

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

           
            
            //m_LevelTime += Time.time;
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
            if (EnemyWavesManager.Instance.currentWave.next!=null) EnemyWavesManager.Instance.currentWave.next = null;
            
        }
    }
}
