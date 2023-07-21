
using UnityEngine;


namespace BallistaShooter
{
    public class TDLevelController : LevelController
    {
        private int levelScore =3;
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);
            };

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                Debug.Log("level time"+ ReferenceTime + " spend time "+ TDPlayer.Instance.m_CurrentTime.ToString());
                if (ReferenceTime <= TDPlayer.Instance.m_CurrentTime) levelScore -= 1;
                Debug.Log("minus time");
                Debug.Log("level csore"+levelScore);
                MapCompletion.SaveEpisodeResult(levelScore);
            }
            );

            void LifeScoreChange(int _)
            {
                levelScore -= 1;
                Debug.Log("minus life");
                TDPlayer.Instance.OnLifeUpdate -= LifeScoreChange;
            }
            TDPlayer.Instance.OnLifeUpdate += LifeScoreChange;


            m_LevelTime += Time.time;

        }

        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<TD_PatrolController>().m_MovePosition = Vector3.zero;
            }

            void DisableAll<T>() where T:MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();


        }

        
    }
}
