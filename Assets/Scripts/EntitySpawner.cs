
using UnityEngine;


namespace CannonShooter
{

    public class EntitySpawner : Spawner
    {
        /// <summary>
        /// Ссылки на то что спавнить.
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs;

        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }

        /*[SerializeField] private Path m_Path;

        [SerializeField] private EnemyAsset[] m_EnemySettings;
        /// <summary>
        /// Зона спавна.
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// Режим спавна.
        /// </summary>
        public enum SpawnMode
        {
            /// <summary>
            /// На методе Start()
            /// </summary>
            Start,

            /// <summary>
            /// Периодически используя время m_RespawnTime
            /// </summary>
            Loop
        }

        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// Кол-во объектов которые будут разом заспавнены.
        /// </summary>
        [SerializeField] private int m_NumSpawns;

        /// <summary>
        /// Время респавна. Здесь важно заметить что респавн таймер должен быть больше времени жизни объектов.
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        private float m_Timer;

        private void Start()
        {
            if(m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if(m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for(int i = 0; i < m_NumSpawns; i++)
            {
                var prefabToSpawn = m_EntityPrefabs[UnityEngine.Random.Range(0, m_EntityPrefabs.Length)];
                var e = Instantiate(prefabToSpawn);

                e.transform.position = m_Area.RandomInsideZone;

                if(e.TryGetComponent<TD_PatrolController>(out var ai))
                {
                    ai.SetPath (m_Path);
                }

                if(e.TryGetComponent<Enemy>(out var en))
                {
                    en.Use(m_EnemySettings[Random.Range(0,2)]);
                }
            }
        }*/
    }
}