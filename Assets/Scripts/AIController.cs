using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace CannonShooter
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent m_NavMeshAgent;

        [SerializeField] private float m_Speed;
        
        [SerializeField] private UnityEvent OnPathEnd;

        private Path path;
        private int pathIndex;

        private void Start()
        {
            m_NavMeshAgent.avoidancePriority = Random.Range(1, 255);
            m_NavMeshAgent.speed = m_Speed;
        }

        private void Update()
        {
            if (path == null) return;

            bool isInsidePatrolZone = (path[pathIndex].transform.position - transform.position).sqrMagnitude < path[pathIndex].Radius * path[pathIndex].Radius;

            if (isInsidePatrolZone)
            {
                GetNewPoint();
            }
        }
        

        public void SetPath(Path newPath)
        {
            path = newPath;
            pathIndex = 0;
            m_NavMeshAgent.SetDestination(path[pathIndex].transform.position);
        }

        public void SetSlowed(bool slow)
        {
            m_NavMeshAgent.speed = slow ? m_Speed * 0.5f : m_Speed;

            if (m_NavMeshAgent.speed < 1)
            {
                m_NavMeshAgent.speed = 1;
            }
        }

        public void ChangeNavigationState(bool stop)
        {
            switch (stop)
            {
                case false:
                    m_NavMeshAgent.SetDestination(path[pathIndex].transform.position);
                    break;
                
                case true:
                    m_NavMeshAgent.SetDestination(transform.position);
                    break;
            }
        }

        private void GetNewPoint()
        {
            pathIndex++;
            if (path.Lenght > pathIndex)
            {
                m_NavMeshAgent.SetDestination(path[pathIndex].transform.position);
            }
            else
            {
                GetComponent<Destructible>().DeathEffectUse();;
                CameraShake.Instance.ShakeCamera();
                OnPathEnd.Invoke();
                Destroy(gameObject);
            }
        }
        

    }
}

