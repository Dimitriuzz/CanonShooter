using UnityEngine;
using UnityEngine.Serialization;

namespace CannonShooter
{
    public class EnemySpawner : Spawner
    {
        
        [FormerlySerializedAs("m_EnemysPrefab")] [SerializeField] private Enemy[] m_EnemyPrefabs;
        [SerializeField] private Path m_Path;

        protected override GameObject GenerateSpawnedEntity()
        {
            var enemy = Instantiate(m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)]);
            enemy.GetComponent<AIController>().SetPath(m_Path);

            return enemy.gameObject;
        }
    }
}