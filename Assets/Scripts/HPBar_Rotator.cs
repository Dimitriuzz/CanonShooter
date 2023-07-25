using UnityEngine;

public class HPBar_Rotator : MonoBehaviour
{
    private Vector3 target;

    private void Start()
    {
        target = new Vector3(0, transform.position.y, 0);
    }

    private void Update()
    {
        transform.LookAt(target);
    }
}
