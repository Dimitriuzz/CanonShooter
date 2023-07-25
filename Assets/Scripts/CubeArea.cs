using UnityEngine;

namespace CannonShooter
{
    public class CubeArea : MonoBehaviour
    {
        [SerializeField] private Vector3 m_Size;

        public Vector3 RandomInsideZone()
        {
            var offsetX = Random.Range(-m_Size.x / 2, m_Size.x / 2);
            var offsetZ = Random.Range(-m_Size.z / 2, m_Size.z / 2);
            
            return transform.position + new Vector3(offsetX, 0, offsetZ);
        }


#if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color= GizmoColor;
            Gizmos.DrawCube(transform.position, m_Size);
        }
#endif
    }
}