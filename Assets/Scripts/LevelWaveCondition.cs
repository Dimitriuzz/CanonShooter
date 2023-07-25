
using UnityEngine;


namespace CannonShooter
{

    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

        private void Start()
        {
            EnemyWavesManager.OnAllWavesDead += () =>
              {
                  isCompleted = true;
                  LevelController.Instance.LevelCompleted();
                  Debug.Log("is completed");
              };
        }
        public bool IsCompleted { get { return isCompleted; } }
    }
}
