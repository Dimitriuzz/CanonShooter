
using UnityEngine;


namespace CannonShooter
{

    public class Path : MonoBehaviour
    {
        [SerializeField] private AIPointPatrol[] points;
        [SerializeField] private CubeArea startArea;
        public CubeArea StartArea { get { return startArea; } }

        public int Lenght { get => points.Length; }

        public AIPointPatrol this[int i] { get => points[i]; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach (var point in points)
                Gizmos.DrawSphere(point.transform.position, point.Radius);
        }
    }
}
