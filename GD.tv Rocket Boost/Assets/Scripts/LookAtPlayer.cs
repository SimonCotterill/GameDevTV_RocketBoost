using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public Transform target;

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.position);
        }
    }
}
