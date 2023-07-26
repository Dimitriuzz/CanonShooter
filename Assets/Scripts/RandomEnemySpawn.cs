
using UnityEngine;
using System;


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
            nextSpawn= UnityEngine.Random.Range(0.5f, 3);
            time = 0;
        }

        private void Update()
        {
            time += Time.deltaTime;
            if(time>=nextSpawn)
            { 
                  
                        var e = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0,enemyPrefabs.Length)],
                                             paths[UnityEngine.Random.Range(0, paths.Length)].StartArea.RandomInsideZone(),
                                                    paths[UnityEngine.Random.Range(0, paths.Length)].StartArea.transform.rotation);
                                              

                        e.GetComponent<AIController>().SetPath(paths[UnityEngine.Random.Range(0, paths.Length)]);

                time = 0;
                nextSpawn = UnityEngine.Random.Range(0.5f, 3);
            }
            
           
        }

    }
}
