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
            Debug.Log(transform.name+" path index " + pathIndex);
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
            Debug.Log(transform.name + " get new point " + pathIndex+" from"+path.Lenght);
            if (path.Lenght > pathIndex)
            {
                m_NavMeshAgent.SetDestination(path[pathIndex].transform.position);
                Debug.Log(transform.name + " new point set " + pathIndex + " from" + path.Lenght);
            }
            else
            {
                Debug.Log(transform.name + " have to be destroyed " + pathIndex + " from" + path.Lenght);
                GetComponent<Destructible>().DeathEffectUse();;
                CameraShake.Instance.ShakeCamera();
                //OnPathEnd.Invoke();
                Destroy(gameObject);
            }
        }
        

    }
}

