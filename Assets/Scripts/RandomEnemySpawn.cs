
using UnityEngine;



namespace CannonShooter
{
    public class RandomEnemySpawn : MonoSingleton<EnemyWavesManager>
    {
        [SerializeField] private Path[] paths;
        [SerializeField] private Enemy[] enemyPrefabs;

        private float nextSpawn;
        private float time;
       

        private void Start()
        {
            nextSpawn= Random.Range(2, 3);
            time = 0;
        }

        private void Update()
        {
            time += Time.deltaTime;
            Debug.Log(time + " " + nextSpawn);
            if(time>=nextSpawn)
            { 
                  
                        var e = Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)],
                                             paths[Random.Range(0, paths.Length)].StartArea.RandomInsideZone(),
                                                    paths[Random.Range(0, paths.Length)].StartArea.transform.rotation);

                var z = Random.Range(0, paths.Length);
                Debug.Log(z);
                        e.GetComponent<AIController>().SetPath(paths[z]);

                time = 0;
                nextSpawn = Random.Range(0.5f, 3);
            }
            
           
        }

    }
}
