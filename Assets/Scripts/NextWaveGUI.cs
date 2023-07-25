
using UnityEngine;
using UnityEngine.UI;

namespace CannonShooter
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;
        private EnemyWavesManager manager;
        private float timeToNextWave;
        // Start is called before the first frame update
        void Start()
        {
            manager = FindObjectOfType<EnemyWavesManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        private void Update()
        {
            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;
           
            bonusAmount.text = bonus.ToString(); 
            timeToNextWave -= Time.deltaTime;
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }
    }
}