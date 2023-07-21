using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace BallistaShooter
{
    /// <summary>
    /// 2Д зона с радиусом.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        /// <summary>
        /// Радиус 2д зоны.
        /// </summary>
        [SerializeField] private float m_Radius;
        [SerializeField] private Vector3 m_Center;
        [SerializeField] private Vector3 m_Size;


        public float Radius => m_Radius;
        
        /// <summary>
        /// Возвращает рандомную позицию внутри круга.
        /// </summary>
        public Vector3 RandomInsideZone
        {
            get
            {
                return (Vector3) m_Center+ new Vector3(Random.Range(-m_Size.x/2,m_Size.x/2),0, Random.Range(-m_Size.z / 2, m_Size.z / 2));
            }
        }

        public bool IsInside(Vector3 p)
        {
            return ((Vector3)transform.position - p).sqrMagnitude < m_Radius * m_Radius;
        }

        /// <summary>
        /// Здесь отрисовка через хендлы в редакторе, для удобства визуализации.
        /// Не забываем утащить апи редактора под дефайны как и using в начале файла.
        /// </summary>
#if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color= new Color(0, 1, 0, 0.3f); ;
            Gizmos.DrawCube(m_Center, m_Size);
        }
#endif
    }
}