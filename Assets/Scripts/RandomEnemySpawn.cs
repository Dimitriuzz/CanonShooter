
using UnityEngine;



namespace CannonShooter
{
    public class RandomEnemySpawn : MonoSingleton<EnemyWavesManager>
    {
        [SerializeField] private Path[] paths;
        [SerializeField] private Enemy[] enemyPrefabs;

        private float nextSpawn;
        private float m_Time;
       

        private void Start()
        {
            nextSpawn= Random.Range(2, 3);
            m_Time = 0;
        }

        private void Update()
        {
            m_Time += Time.deltaTime;
            //Debug.Log(m_Time + " " + nextSpawn);
            if(m_Time>=nextSpawn)
            {
                m_Time = 0;
                //Debug.Log(m_Time);
                nextSpawn = Random.Range(2, 3);

                var e = Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)],
                                             paths[Random.Range(0, paths.Length)].StartArea.RandomInsideZone(),
                                                    paths[Random.Range(0, paths.Length)].StartArea.transform.rotation);

                var z = Random.Range(0, paths.Length);
                //Debug.Log(z);
                e.GetComponent<AIController>().SetPath(paths[z]);

                
            }
            
           
        }

    }
}
